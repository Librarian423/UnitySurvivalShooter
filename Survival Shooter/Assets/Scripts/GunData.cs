using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/GunData", fileName = "GunData")] 
public class GunData : ScriptableObject
{    
    public AudioClip shotClip; // 발사 소리
    public float attackDmg = 25; // 공격력
    public float attackDistance = 50f; // 사정거리
    public float attackDelay = 0.12f; // 총알 발사 간격
}
