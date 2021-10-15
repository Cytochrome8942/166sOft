using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionHpBar : EnemyHpBar
{
	private MinionInfo minionInfo;

	public void Initialize(MinionInfo minionInfo)
	{
		this.minionInfo = minionInfo;
		fullHp = minionInfo.hp;
		Init();
	}

	// Update is called once per frame
	protected override void Update()
    {
		currentHp = minionInfo.hp;
		base.Update();
    }
}
