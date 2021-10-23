using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMeleeAttack : MinionAttack
{
	protected override void AttackTarget()
	{
		StartCoroutine(Rotate());
		StartCoroutine(UnlockAttacking());
	}


	//애니메이션에 달기
	public void Damage()
	{
		if(minionInfo.target == null)
		{
			minionInfo.attacking = true;
			return;
		}
		else if (minionInfo.target.CompareTag("Minion"))
		{
			minionInfo.target.GetComponent<MinionControl>().Damaged(minionInfo.attackDamage);
		}
		else if (minionInfo.target.CompareTag("Player"))
		{
			minionInfo.target.GetComponent<CharacterControl>().Damaged(minionInfo.attackDamage);
		}
		minionInfo.attacking = false;
	}

	private IEnumerator UnlockAttacking()
	{
		yield return new WaitForSeconds(minionInfo.attackSpeed);
		Damage();
	}
}
