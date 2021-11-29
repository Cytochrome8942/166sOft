using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/NexusInfo")]
public class NexusInfo : ScriptableObject
{
    public int team;

    public float hp;
    public float physicalDefence;
    public float magicalDefence;
}
