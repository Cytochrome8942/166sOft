using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSiegeAttack : MinionAttack
{
	[System.NonSerialized]
	public GameObject siegeBulletHolder;

	protected override void AttackTarget()
	{
		StartCoroutine(Rotate());
		StartCoroutine(UnlockAttacking());
	}
	public void Damage()
	{
		if (minionInfo.target == null)
		{
			minionInfo.attacking = true;
			return;
		}
		else if(!minionInfo.target.CompareTag("Path"))
		{
			siegeBulletHolder.transform.GetChild(0).GetComponent<MinionBullet>().Enable(minionInfo.target, transform.position.YZero() + new Vector3(0, 1, 0), minionInfo.attackDamage);
		}
		minionInfo.attacking = false;
	}

	private IEnumerator UnlockAttacking()
	{
		yield return new WaitForSeconds(minionInfo.attackSpeed);
		Damage();
	}
}
