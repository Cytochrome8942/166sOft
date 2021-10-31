using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/EventData")]
public class EventData : ScriptableObject
{
//    [System.NonSerialized]
    public UnityEvent eventValue = new UnityEvent();

    public void Invoke()
	{
        eventValue.Invoke();
	}
}
