using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSensor : EnemySensor
{
	[System.NonSerialized]
	public TowerInfo towerInfo;

	public void Initialize(TowerInfo towerInfo)
	{
		this.towerInfo = towerInfo;
		team = towerInfo.team;
		GetComponent<SphereCollider>().radius = towerInfo.attackRange;
	}

	public void Update()
	{
		if (towerInfo.target == null)
		{
			SetNearestTarget();
		}
		towerInfo.target = target;
	}
}
