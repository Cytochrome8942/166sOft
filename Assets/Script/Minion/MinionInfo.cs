using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/MinionInfo")]
public class MinionInfo : ScriptableObject
{
    [System.NonSerialized]
    public int team;

    public float hp = 100;
    public float attackSpeed = 2;
    public float attackRange = 1;
    public float targetRange = 5; // 대상을 타겟으로 삼을 거리
    public float attackDamage = 5;

    public bool attacking = false;

    [System.NonSerialized]
    public Transform target;

    [System.NonSerialized]
    public UnityEvent deathEvent = new UnityEvent();
}
