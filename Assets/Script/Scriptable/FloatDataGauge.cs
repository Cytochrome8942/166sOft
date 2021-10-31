using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatDataGauge : MonoBehaviour
{
	public UnityEngine.UI.Image fullGauge;

	public FloatData target;
	public FloatData full;

	private void Update()
	{
		fullGauge.fillAmount = target.get() / full.get();
	}
}
