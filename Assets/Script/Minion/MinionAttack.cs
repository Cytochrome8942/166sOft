using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Bolt;

public abstract class MinionAttack : EntityEventListener<ICusPlayerState>
{
	protected MinionInfo minionInfo;

	private float attackClock = 0;

	public void Initialize(MinionInfo minionInfo)
	{
		this.minionInfo = minionInfo;
		minionInfo.deathEvent.AddListener(Die);
	}

	private void Die()
	{
		enabled = false;
	}

	protected abstract void AttackTarget();

	public override void SimulateOwner()
	{
		attackClock += Time.deltaTime;
		if (minionInfo.target != null && attackClock > minionInfo.attackSpeed && minionInfo.attacking)
		{
			attackClock = 0f;
			AttackTarget();
		}
	}

	protected IEnumerator Rotate()
	{
		var startRotation = transform.rotation;
		var targetRotation = Quaternion.LookRotation(minionInfo.target.position.YZero() - transform.position.YZero());

		float clock = 0f;
		while (clock < 1f)
		{
			clock += Time.deltaTime * 4;
			transform.rotation = Quaternion.Slerp(startRotation, targetRotation, clock);
			yield return null;
		}
	}
}
