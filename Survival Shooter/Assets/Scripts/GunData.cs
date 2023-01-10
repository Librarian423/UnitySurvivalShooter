using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/GunData", fileName = "GunData")] 
public class GunData : ScriptableObject
{    
    public AudioClip shotClip; // �߻� �Ҹ�
    public float attackDmg = 25; // ���ݷ�
    public float attackDistance = 50f; // �����Ÿ�
    public float attackDelay = 0.12f; // �Ѿ� �߻� ����
}
