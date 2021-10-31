using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
	[System.NonSerialized]
	public TowerInfo towerInfo;

	private float attackClock = 0;

	private GameObject towerBulletHolder;

	public void Initialize(TowerInfo towerInfo, GameObject towerBulletHolder)
	{
		this.towerInfo = towerInfo;
		this.towerBulletHolder = towerBulletHolder;
	}

	private void Update()
	{
		attackClock += Time.deltaTime;
		if (towerInfo.target != null && attackClock >= towerInfo.attackSpeed)
		{
			attackClock = 0f;
			towerBulletHolder.transform.GetChild(0).GetComponent<EnemyBullet>().Enable(towerInfo.target, transform.position.YZero() + new Vector3(0, 3, 0), towerInfo.attackDamage);
		}
	}
}
