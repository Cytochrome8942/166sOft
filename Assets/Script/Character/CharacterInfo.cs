using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterInfo")]
public class CharacterInfo : ScriptableObject
{
    public TextAsset mobadata;
    // 캐릭터 고유번호
    public int team = 1;

    public FloatData exp;
    public FloatData expMax;

    public IntData level;

    public FloatData fullHp;
    public FloatData hp;
    public FloatData fullMp;
    public FloatData mp;
    public FloatData hpRegen;
    public FloatData mpRegen;

    public bool isPhysical = true;
    public FloatData physicalAttack;
    public FloatData physicalDefence;
    public FloatData magicalAttack;
    public FloatData magicalDefence;

    public bool isMelee = false;
    public float meleeRange = 1;
    public float distancedRange = 5;
    public FloatData attackSpeed;
    public float attackSpeedAfter = 0.8f;
    public float attackSpeedBefore = 0.2f; // 선딜

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

    public List<string> levelData;

    public void Reset()
    {
        levelData = new List<string>();
        string[] line = mobadata.text.Substring(0, mobadata.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            levelData.Add(line[i]);
        }
        level.set(0);
        exp.set(0);
        hp.set(0);
        fullHp.set(0);
        mp.set(0);
        fullMp.set(0);
        LevelUp();
    }

    public void LevelUp()
    {
        Debug.Log(levelData[level.get()]);
        float[] floatData = Array.ConvertAll(levelData[level.get()].Split('\t'), float.Parse);

        expMax.set(floatData[0]);
        hp.add(floatData[1] - fullHp.get());
        fullHp.set(floatData[1]);
        mp.add(floatData[2] - fullMp.get());
        fullMp.set(floatData[2]);
        hpRegen.set(floatData[4]);
        mpRegen.set(floatData[5]);
        physicalAttack.set(floatData[6]);
        physicalDefence.set(floatData[7]);
        magicalAttack.set(floatData[8]);
        magicalDefence.set(floatData[9]);
        meleeRange = floatData[10] / 100;
        distancedRange = floatData[11] / 100;
        moveSpeed = floatData[12] / 100;
        attackSpeed.set(floatData[13]);
        attackSpeedAfter = attackSpeed.get() * 0.8f;
        attackSpeedBefore = attackSpeed.get() * 0.2f;

        level.set(level.get() + 1);
    }
}

