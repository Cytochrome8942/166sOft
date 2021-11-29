using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHpBar : HpBar
{
	private TowerInfo towerInfo;

	public void Initialize(TowerInfo towerInfo)
	{
		this.towerInfo = towerInfo;
		barHeight = 5f;
		fullHp = towerInfo.hp;
		Init();
	}

	// Update is called once per frame
	protected override void Update()
	{
		currentHp = towerInfo.hp;
		base.Update();
	}
}
