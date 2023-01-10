using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : LivingEntity
{
    public LayerMask whatIsTarget; // 추적 대상 레이어
    private LivingEntity targetEntity; // 추적할 대상
    public NavMeshAgent pathFinder; // 경로계산 AI 에이전트
    public GameObject player;

    public Animator enemyAnimator;
    public ParticleSystem hitEffect; // 피격시 재생할 파티클 효과

    public AudioSource enemyAudioSource;
    public AudioClip deathClip;
    public AudioClip hurtClip;

    public float health;
    public float damage;
    public float speed;

    public float attackTime;
    public float timeBetAttack = 0.5f; // 공격 간격
    public float lastAttackTime;

    public float range = 10f;

    public float sinkSpeed = 2.5f;

    private bool isSinking = false;

    private bool hasTarget
    {
        get
        {
            // 추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
            return targetEntity != null && !targetEntity.dead;
        }
    }

    // 적 AI의 초기 스펙을 결정하는 셋업 메서드
    
    public void Setup(float newHealth, float newDamage, float newSpeed, Color skinColor)
    {
        startingHealth = newHealth;
        damage = newDamage;
        pathFinder.speed = newSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        // 게임 오브젝트 활성화와 동시에 AI의 추적 루틴 시작
        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            return;
        }

        enemyAnimator.SetBool("HasTarget", hasTarget);
    }
    // 주기적으로 추적할 대상의 위치를 찾아 경로를 갱신
    private IEnumerator UpdatePath()
    {
        //if (isSinking)
        //{
        //    // 바닥 밑으로 내려가는 싱크효과
        //    transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        //}
        // 살아있는 동안 무한 루프
        while (!dead)
        {
            if (hasTarget)
            {
                //Debug.Log("true");
                pathFinder.isStopped = false;
                pathFinder.SetDestination(targetEntity.transform.position);
            }
            else
            {
                //Debug.Log("false");
                pathFinder.isStopped = true;

                var colliders = Physics.OverlapSphere(transform.position, range, whatIsTarget);
                foreach (var collider in colliders)
                {
                    var entity = collider.GetComponent<LivingEntity>();
                    if (entity != null)
                    {
                        targetEntity = entity;
                        break;
                    }
                }

            }
            // 0.25초 주기로 처리 반복
            yield return new WaitForSeconds(0.25f);
        }
    }

    // 데미지를 입었을때 실행할 처리
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (dead)
        {
            return;
        }

        hitEffect.transform.position = hitPoint;
        hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
        hitEffect.Play();

        enemyAudioSource.PlayOneShot(hurtClip);

        // LivingEntity의 OnDamage()를 실행하여 데미지 적용
        base.OnDamage(damage, hitPoint, hitNormal);


    }

    // 사망 처리
    public override void Die()
    {
        // LivingEntity의 Die()를 실행하여 기본 사망 처리 실행
        base.Die();

        pathFinder.isStopped = true;
        pathFinder.enabled = false;

        enemyAnimator.SetTrigger("Death");
        enemyAudioSource.PlayOneShot(deathClip);

        //var colliders = GetComponents<Collider>();
        //foreach (var collider in colliders)
        //{
        //    collider.enabled = false;
        //}
    }

    private void OnTriggerStay(Collider other)
    { 
        if (dead || Time.time - lastAttackTime < timeBetAttack)
        {
            return;
        }

        // 트리거 충돌한 상대방 게임 오브젝트가 추적 대상이라면 공격 실행
        //var attackTarget = other.GetComponent<LivingEntity>();
        if (player != null && player == targetEntity)
        {
            lastAttackTime = Time.time;

            var hitPoint = other.ClosestPoint(transform.position);
            var hitNormal = transform.position - other.transform.position;
            player.GetComponent<PlayerHealth>().OnDamage(damage, hitPoint, hitNormal);
        }
    }

    private void StartSinking()
    {
        //isSinking = true;
        transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        var colliders = GetComponents<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
        Destroy(gameObject, 2f);

    }
}
