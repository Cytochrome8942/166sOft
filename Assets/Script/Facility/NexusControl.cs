using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class NexusControl : CommonObject
{
	private cakeslice.Outline outline;

	public int team;

	Coroutine dieCoroutine;

	public NexusInfo nexusInfo;

	private int lockVal = 0;

	public override void Attached()
	{
		outline = GetComponentInChildren<cakeslice.Outline>();
		nexusInfo = Instantiate(nexusInfo);
		state.Team = nexusInfo.team;
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
		state.Health = nexusInfo.hp;
		if (nexusInfo.hp <= 0 && dieCoroutine == null)
		{
			dieCoroutine = StartCoroutine(Die());
		}
	}

	public override void OnEvent(bulletHitEvent evnt){
		if(entity.IsOwner){
			Damaged(evnt.Damage);
		}
	}

	private IEnumerator Die()
	{
		// ��� �ִϸ��̼�
		GetComponent<Animator>().SetTrigger("DIE");

		GetComponent<Collider>().enabled = false;

		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}

	private void OnMouseOver()
	{
		if (!(GameManager.instance.playerEntity.GetState<IMinionState>().Team.IsEnemy(state.Team)))
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
		return state.Team.IsEnemy(target);
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