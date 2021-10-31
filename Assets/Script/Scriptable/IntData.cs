using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/IntData")]
public class IntData : DataInterface
{
    private int value = 0;

	public int get()
	{
		return value;
	}
	public void set(int val)
	{
		value = val;
	}
	public override void SetText(TMPro.TextMeshProUGUI target)
	{
		target.text = value.ToString();
	}
}
