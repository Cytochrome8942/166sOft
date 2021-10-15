using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/MinionInfo")]
public class MinionInfo : ScriptableObject
{
    public float hp = 100;
    public float attackSpeed = 2;
    public float attackRange = 1;
    public float targetRange = 5; // 대상을 타겟으로 삼을 거리
    public float attackDamage = 5;
}
