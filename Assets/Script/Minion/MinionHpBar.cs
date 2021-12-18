using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionHpBar : HpBar
{
	private MinionInfo minionInfo;

	public void Initialize(MinionInfo minionInfo)
	{
		this.minionInfo = minionInfo;
		fullHp = minionInfo.hp;
		barHeight = 1f;
		hpBarImage.color = minionInfo.team % 2 == 0 ? Color.red : Color.blue;
		Init();
	}

	// Update is called once per frame
	protected override void Update()
    {
  		currentHp = state.Health;
		base.Update();
    }
}
