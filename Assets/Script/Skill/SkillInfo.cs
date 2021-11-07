using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SkillInfo")]
public class SkillInfo : ScriptableObject
{
    //Common
    public float beforeAttack;
    public float afterAttack;
    public float cooldown;
    public bool isPhysical;
    public float damage;

    public ParticleSystem startParticle;
    public ParticleSystem middleParticle;
    public ParticleSystem endParticle;

    public enum SkillType { Target, Shoot, Circle, Rectangle, Sector};
    public SkillType skillType;
    [SerializeField]
    public Skill skill;
}

[System.Serializable]
public class Skill { }

[System.Serializable]
public class Target : Skill
{
    [SerializeField]
    public float range = 1;
}

[System.Serializable]
public class Shoot : Skill
{
    [SerializeField]
    public bool isPenetrate = true;
    [SerializeField]
    public float speed = 1;
    [SerializeField]
    public float lifeTime = 1;
}


[System.Serializable]
public class Circle : Skill
{
    public float radius = 1;
    public float activeTime = 1;
    public float range = 1;
}

[System.Serializable]
public class Rectangle : Skill
{
    public float width = 1;
    public float length = 1;
    public float activeTime = 1;
}

[System.Serializable]
public class Sector : Skill
{
    public float radius = 1;
    public float angle = 1;
    public float activeTime = 1;
}
