using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHpBar : HpBar
{

	public void Initialize()
	{
		barHeight = 5f;
		fullHp = state.MaxHealth;
		Init();
	}

	// Update is called once per frame
	protected override void Update()
	{
 		currentHp = state.Health;
		base.Update();
	}
}
