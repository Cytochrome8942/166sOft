using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/MinionInfo")]
public class MinionInfo : ScriptableObject
{
    [System.NonSerialized]
    public int team;

    public float hp;
    public float attackSpeed;
    public float attackRange;
    public float targetRange; // ����� Ÿ������ ���� �Ÿ�
    public float attackDamage;

    public float exp = 24;

    // ���Ŀ� ����
    public float expRange = 24;

    public bool attacking = false;

    [System.NonSerialized]
    public Transform target;

    public UnityEvent deathEvent = new UnityEvent();
}
