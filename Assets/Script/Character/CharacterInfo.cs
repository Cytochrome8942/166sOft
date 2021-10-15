using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterInfo")]
public class CharacterInfo : ScriptableObject
{
    public float hp = 100;
    public float attackRange = 5;
    public float attackSpeed = 0.8f;
    public float attackSpeedBefore = 0.2f; // ¼±µô
    public float attackDamage = 10;

    public float moveSpeed = 5;
    public float rotateSpeed = 1000;

    [System.NonSerialized]
    public Vector3 moveTarget;

    [System.NonSerialized]
    public float movableClock = 0f;
    [System.NonSerialized]
    public float rotatableClock = 0f;
    [System.NonSerialized]
    public float attackClock = 0f;
}
