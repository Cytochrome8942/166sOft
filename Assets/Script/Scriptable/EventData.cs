using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/EventData")]
public class EventData : MonoBehaviour
{
    [System.NonSerialized]
    public UnityEvent eventValue = new UnityEvent();
}
