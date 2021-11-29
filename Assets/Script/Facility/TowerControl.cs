using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : CommonObject
{
	[System.NonSerialized]
	public TowerInfo towerInfo;

	private cakeslice.Outline outline;

	public int team;

	Coroutine dieCoroutine;

	public GameObject nextTarget;

	public void Initialize(TowerInfo towerInfo, GameObject towerBulletHolder, bool setTargetable = false)
	{
		this.towerInfo = towerInfo;
		team = towerInfo.team;
		transform.parent.GetComponentInChildren<TowerSensor>().Initialize(towerInfo);
		GetComponent<TowerAttack>().Initialize(towerInfo, towerBulletHolder);
		outline = GetComponentInChildren<cakeslice.Outline>();
		transform.GetComponentInChildren<TowerHpBar>().Initialize(towerInfo);
		targetable = setTargetable;
	}

	public void Damaged(float attack, bool isPhysical = true)
	{
		if (isPhysical)
		{
			towerInfo.hp -= attack.CalculateDamage(towerInfo.physicalDefence);
		}
		else
		{
			towerInfo.hp -= attack.CalculateDamage(towerInfo.magicalDefence);
		}
		if (towerInfo.hp <= 0 && dieCoroutine == null)
		{
			dieCoroutine = StartCoroutine(Die());
		}
	}

	private IEnumerator Die()
	{
		// 사망 애니메이션
		targetable = false;
		GetComponent<Collider>().enabled = false;
		// 경험치 분배
		GetComponent<ExpProvider>().ProvideExp(towerInfo.exp, towerInfo.team);

		if(TryGetComponent(out TowerControl towerControl))
		{
			towerControl.targetable = true;
		}
		else if(TryGetComponent(out NexusControl nexusControl))
		{
			nexusControl.TowerDestroyed();
		}

		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}

	private void OnMouseOver()
	{
		if (targetable && GameManager.instance.playerInfo.team.IsEnemy(towerInfo.team))
		{
			outline.eraseRenderer = false;
		}
	}

	private void OnMouseExit()
	{
		outline.eraseRenderer = true;
	}

	public bool IsEnemy(int target)
	{
		return towerInfo.team.IsEnemy(target);
	}
}
