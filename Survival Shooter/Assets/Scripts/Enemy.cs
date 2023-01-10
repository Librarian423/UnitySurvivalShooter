using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    public LayerMask whatIsTarget;
    private LivingEntity targetEntity;
    public NavMeshAgent pathFinder;
    public Animator enemyAnimator;
    public ParticleSystem hitEffect;

    public AudioSource enemyAudioSource;
    public AudioClip deathClip;
    public AudioClip hurtClip;

    public float health = 100f;
    public float damage = 20f;
    public float speed;

    public float attackTime;
    public float timeBetAttack = 0.5f;
    public float lastAttackTime;

    public float range = 10f;

    private bool hasTarget
    {
        get
        {
            return targetEntity != null && !targetEntity.dead;
        }
    }

    
    public void Setup(float newHealth, float newDamage, float newSpeed, Color skinColor)
    {
        startingHealth = newHealth;
        damage = newDamage;
        pathFinder.speed = newSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
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
    private IEnumerator UpdatePath()
    {
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
            yield return new WaitForSeconds(0.25f);
        }
    }

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

        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        base.Die();

        pathFinder.isStopped = true;
        pathFinder.enabled = false;

        enemyAnimator.SetTrigger("Die");
        enemyAudioSource.PlayOneShot(deathClip);

        var colliders = GetComponents<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    { 
        if (dead || Time.time - lastAttackTime < timeBetAttack)
        {
            return;
        }

        var attackTarget = other.GetComponent<LivingEntity>();
        if (attackTarget != null && attackTarget == targetEntity)
        {
            lastAttackTime = Time.time;

            var hitPoint = other.ClosestPoint(transform.position);
            var hitNormal = transform.position - other.transform.position;
            attackTarget.OnDamage(damage, hitPoint, hitNormal);
        }
    }
}
