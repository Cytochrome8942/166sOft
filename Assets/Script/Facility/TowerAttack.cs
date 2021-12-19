using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class TowerAttack : EntityBehaviour
{
	[System.NonSerialized]
	public TowerInfo towerInfo;
	public GameObject towerBullet;

	private float attackClock = 0;

	public void Initialize(TowerInfo towerInfo)
	{
		this.towerInfo = towerInfo;
	}

	public override void SimulateOwner()
	{
		attackClock += Time.deltaTime;
		if (towerInfo.target != null && attackClock >= towerInfo.attackSpeed)
		{
			attackClock = 0f;
			var bullet = BoltNetwork.Instantiate(towerBullet);
			bullet.GetComponent<EnemyBullet>().Enable(towerInfo.target, transform.position.YZero() + new Vector3(0, 3, 0), towerInfo.attackDamage);
		}
	}
}
