using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class MinionCasterAttack : MinionAttack
{
	public GameObject minionBullet;

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
		else if (!minionInfo.target.CompareTag("Path"))
		{
			if(entity.IsOwner){
				var bullet = BoltNetwork.Instantiate(minionBullet);
				bullet.GetComponent<EnemyBullet>().Enable(minionInfo.target, transform.position.YZero() + new Vector3(0, 1, 0), minionInfo.attackDamage);
			}
		}
		minionInfo.attacking = false;
	}

	private IEnumerator UnlockAttacking()
	{
		yield return new WaitForSeconds(minionInfo.attackSpeed);
		Damage();
	}
}
