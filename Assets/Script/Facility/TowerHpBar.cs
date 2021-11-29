using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusHpBar : HpBar
{
	private NexusInfo nexusInfo;

	public void Initialize(NexusInfo nexusInfo)
	{
		this.nexusInfo = nexusInfo;
		barHeight = 5f;
		fullHp = nexusInfo.hp;
		Init();
	}

	// Update is called once per frame
	protected override void Update()
	{
		currentHp = nexusInfo.hp;
		base.Update();
	}
}
