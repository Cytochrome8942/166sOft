using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/BoolData")]
public class BoolData : ScriptableObject
{
	private bool value;

	public bool get()
	{
		return value;
	}
	public void set(bool val)
	{
		value = val;
	}
}
