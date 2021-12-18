using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class TowerAttack : EntityBehaviour
{
	[System.NonSerialized]
	public TowerInfo towerInfo;
	public GameObject redBullet;
	public GameObject blueBullet;

	private float attackClock = 0;

	private GameObject towerBulletHolder;

	public void Initialize(TowerInfo towerInfo, GameObject towerBulletHolder)
	{
		this.towerInfo = towerInfo;
		this.towerBulletHolder = towerBulletHolder;
	}

	public override void SimulateOwner()
	{
		attackClock += Time.deltaTime;
		if (towerInfo.target != null && attackClock >= towerInfo.attackSpeed)
		{
			attackClock = 0f;
			//towerBulletHolder.transform.GetChild(0).GetComponent<EnemyBullet>().Enable(towerInfo.target, transform.position.YZero() + new Vector3(0, 3, 0), towerInfo.attackDamage);
			if(towerInfo.team == 0){
				var bullet = BoltNetwork.Instantiate(redBullet);
				bullet.GetComponent<EnemyBullet>().Enable(towerInfo.target, transform.position.YZero() + new Vector3(0, 3, 0), towerInfo.attackDamage);
			}
			else{
				var bullet = BoltNetwork.Instantiate(blueBullet);
				bullet.GetComponent<EnemyBullet>().Enable(towerInfo.target, transform.position.YZero() + new Vector3(0, 3, 0), towerInfo.attackDamage);
			}
		}
	}
}
