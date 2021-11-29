using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusControl : CommonObject
{
	private cakeslice.Outline outline;

	public int team;

	Coroutine dieCoroutine;

	public NexusInfo nexusInfo;

	private int lockVal = 0;

	private void Awake()
	{
		outline = GetComponentInChildren<cakeslice.Outline>();
		nexusInfo = Instantiate(nexusInfo);
		transform.GetComponentInChildren<NexusHpBar>().Initialize(nexusInfo);
	}

	public void Damaged(float attack, bool isPhysical = true)
	{
		if (isPhysical)
		{
			nexusInfo.hp -= attack.CalculateDamage(nexusInfo.physicalDefence);
		}
		else
		{
			nexusInfo.hp -= attack.CalculateDamage(nexusInfo.magicalDefence);
		}
		if (nexusInfo.hp <= 0 && dieCoroutine == null)
		{
			dieCoroutine = StartCoroutine(Die());
		}
	}

	private IEnumerator Die()
	{
		// 사망 애니메이션
		GetComponent<Animator>().SetTrigger("DIE");

		GetComponent<Collider>().enabled = false;

		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}

	private void OnMouseOver()
	{
		if (GameManager.instance.playerInfo.team.IsEnemy(nexusInfo.team))
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
		return nexusInfo.team.IsEnemy(target);
	}

	public void TowerDestroyed()
	{
		++lockVal;
		if(lockVal == 2)
		{
			targetable = true;
		}
	}
}