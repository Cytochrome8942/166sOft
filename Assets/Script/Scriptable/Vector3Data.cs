using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Vector3Data")]
public class Vector3Data : ScriptableObject
{
    private Vector3 value;

	public Vector3 get()
	{
		return value;
	}
	public void set(Vector3 val)
	{
		value = val;
	}
}