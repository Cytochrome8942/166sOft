using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/FloatData")]
public class FloatData : DataInterface
{
	[SerializeField]
    private float value = 0;

    public float get()
	{
		return value;
	}
	public void set(float val)
	{
		value = val;
	}
	public void add(float val)
	{
		value += val;
	}

	public override void SetText(TMPro.TextMeshProUGUI target) {
		target.text = value.ToString();
	}
}
