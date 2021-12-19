using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionHpBar : HpBar
{
	public void Initialize()
	{
		fullHp = state.MaxHealth;
		barHeight = 1f;
		Init();
	}

	// Update is called once per frame
	protected override void Update()
    {
  		currentHp = state.Health;
		base.Update();
    }
}
