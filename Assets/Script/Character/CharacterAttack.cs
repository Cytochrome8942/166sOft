using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [System.NonSerialized]
    public CharacterInfo characterInfo;

	[System.NonSerialized]
	public CharacterMove characterMove;

    //원거리 / 근거리?
    public GameObject characterBulletHolder;

	private Coroutine attackCoroutine;

	//공격타겟
	private Transform target;

	private void Awake()
	{
		characterMove = GetComponent<CharacterMove>();
	}

	private void Update()
	{
		// 타겟 있음, 사거리 이내, 공격가능 : 공격
		if (target != null && Vector3.Distance(transform.position.YZero(), target.position.YZero()) < characterInfo.attackRange 
			&& characterInfo.attackClock > 0f)
		{
			characterInfo.attackClock = -characterInfo.attackSpeed;
			attackCoroutine = StartCoroutine(AttackTarget());
		}
		// 타겟 있음, 사거리 이내, 공격 불가능 : 대기
		else if (target != null && Vector3.Distance(transform.position.YZero(), target.position.YZero()) < characterInfo.attackRange
			&& characterInfo.attackClock <= 0f)
		{
			characterInfo.moveTarget = transform.position;
		}
		// 타겟 있음, 사거리 밖, 공격가능 혹은 불가능 : 최대 사거리까지 이동
		else if (target != null && Vector3.Distance(transform.position.YZero(), target.position.YZero()) > characterInfo.attackRange)
		{
			characterInfo.moveTarget = target.position;
		}
		characterInfo.attackClock += Time.deltaTime;
	}

	public void AttackTargetSet(Transform target)
	{
		this.target = target;
	}

	private IEnumerator AttackTarget()
	{
		characterInfo.moveTarget = transform.position;
		characterMove.MoveLock(characterInfo.attackSpeedBefore, true, true);

		//선딜동안 적 바라보기
		Vector3 moveTarget = target.position.YZero();
		Quaternion startedRotation = transform.rotation;
		Vector3 rotateTarget = (moveTarget - transform.position).normalized;
		Quaternion targetRotation = Quaternion.LookRotation(new Vector3(rotateTarget.x, 0, rotateTarget.z));
		float clock = 0f;
		while (clock < characterInfo.attackSpeedBefore)
		{
			clock += Time.deltaTime;
			transform.rotation = Quaternion.Slerp(startedRotation, targetRotation, clock * (1 / characterInfo.attackSpeedBefore));
			yield return null;
		}

		characterBulletHolder.transform.GetChild(0).GetComponent<CharacterBullet>().Enable(target, transform.position.YZero() + new Vector3(0, 1, 0));
	}

	public void CancelAttack()
	{
		target = null;
		if(attackCoroutine != null)
		{
			StopCoroutine(attackCoroutine);
		}
	}
}
