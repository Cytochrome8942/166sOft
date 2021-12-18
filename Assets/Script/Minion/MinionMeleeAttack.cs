using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class MinionMeleeAttack : MinionAttack
{
	protected override void AttackTarget()
	{
		StartCoroutine(Rotate());
		StartCoroutine(UnlockAttacking());
	}


	//�ִϸ��̼ǿ� �ޱ�
	public void Damage()
	{
		if(minionInfo.target == null)
		{
			minionInfo.attacking = true;
			return;
		}
		else if (minionInfo.target.CompareTag("Minion"))
		{
			var e = bulletHitEvent.Create(minionInfo.target.GetComponentInParent<BoltEntity>());
			e.Damage = minionInfo.attackDamage;
			e.Send();
		}
		else if (minionInfo.target.CompareTag("Player"))
		{
			var e = bulletHitEvent.Create(minionInfo.target.GetComponent<BoltEntity>());
			e.Damage = minionInfo.attackDamage;
			e.Send();
		}
		minionInfo.attacking = false;
	}

	private IEnumerator UnlockAttacking()
	{
		yield return new WaitForSeconds(minionInfo.attackSpeed);
		Damage();
	}
}
