using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SkillInfo")]
public class SkillInfo : ScriptableObject
{
    //Common
    public float beforeAttack = 0.5f;
    public float afterAttack = 0.5f;
    public float cooldown = 5f;
    public bool isPhysical = true;
    public float damageRate = 1f;
    public float damage = 100f;

    public Particle startParticle;
    public Particle middleParticle;
    public Particle endParticle;

    public enum SkillType { Target, Shoot, Circle, Rectangle, Sector};
    public SkillType skillType;

    public bool boolValue = true;
    public float value1 = 1;
    public float value2 = 1;
    public float value3 = 1;
}

//Target - range;

//Shoot : isPenetrate, speed, lifeTime

//Circle : radius, activeTime, range

//Rectangle : width, length, activeTime

//Sector : radius, angle, activeTime

