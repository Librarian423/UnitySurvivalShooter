using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDummy : LivingEntity
{
   
    //public AudioClip deathClip; // 사망 소리
    //public AudioClip hitClip; // 피격 소리
    //public AudioClip itemPickupClip; // 아이템 습득 소리

    //private AudioSource playerAudioPlayer; // 플레이어 소리 재생기
    //private Animator playerAnimator; // 플레이어의 애니메이터

   

    private void Awake()
    {
        // 사용할 컴포넌트를 가져오기
        //playerAudioPlayer = GetComponent<AudioSource>();
        //playerAnimator = GetComponent<Animator>();
        
    }

    protected override void OnEnable()
    {
        // LivingEntity의 OnEnable() 실행 (상태 초기화)
        base.OnEnable();

        //healthSlider.gameObject.SetActive(true);
        //healthSlider.value = health / startingHealth;

        //playerMovement.enabled = true;
        //playerShooter.enabled = true;


    }

    // 체력 회복
   
    //public override void RestoreHealth(float newHealth)
    //{
    //    // LivingEntity의 RestoreHealth() 실행 (체력 증가)
    //    base.RestoreHealth(newHealth);
    //    healthSlider.value = health / startingHealth;
    //}

    // 데미지 처리
   
    //public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    //{
    //    if (dead)
    //        return;

    //    playerAudioPlayer.PlayOneShot(hitClip);

    //    // LivingEntity의 OnDamage() 실행(데미지 적용)
    //    base.OnDamage(damage, hitPoint, hitDirection);
    //    healthSlider.value = health / startingHealth;
    //}

    //// 사망 처리
    //public override void Die()
    //{
    //    // LivingEntity의 Die() 실행(사망 적용)
    //    base.Die();

    //    healthSlider.gameObject.SetActive(false);
    //    playerAudioPlayer.PlayOneShot(deathClip);
    //    playerAnimator.SetTrigger("Die");

    //    playerMovement.enabled = false;
    //    playerShooter.enabled = false;

    //    Invoke("Respawn", 5f);
    //}

   

    
}