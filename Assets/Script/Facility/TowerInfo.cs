using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TowerInfo")]
public class TowerInfo : ScriptableObject
{
    public int team;

    public float hp;
    public float attackSpeed;
    public float attackRange;
    public float targetRange; // ����� Ÿ������ ���� �Ÿ�
    public float attackDamage;
    public float physicalDefence;
    public float magicalDefence;

    public float exp;

	public Transform target;

    [System.NonSerialized]
	public EventData towerEvent;
}
