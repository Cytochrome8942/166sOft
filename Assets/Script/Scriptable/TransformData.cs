using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TransformData")]
public class TransformData : ScriptableObject
{
    private Transform value;

	public Transform get()
	{
		return value;
	}
	public void set(Transform val)
	{
		value = val;
	}
}
