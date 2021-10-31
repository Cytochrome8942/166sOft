using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataShow : MonoBehaviour
{
	private TMPro.TextMeshProUGUI targetText;

	public DataInterface target;

	private void Awake()
	{
		targetText = GetComponent<TMPro.TextMeshProUGUI>();
	}

	private void Update()
	{
		target.SetText(targetText);
	}
}
