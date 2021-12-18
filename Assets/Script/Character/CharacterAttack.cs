using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class CharacterAttack : EntityBehaviour<IMinionState>
{
    [System.NonSerialized]
    public CharacterInfo characterInfo;

	[System.NonSerialized]
	public CharacterMove characterMove;

    //���Ÿ� / �ٰŸ�?
	public GameObject characterBullet;
	private Coroutine attackCoroutine;

	//����Ÿ��
	private Transform target;

	private void Awake()
	{
		characterMove = GetComponent<CharacterMove>();
	}

	public override void SimulateOwner()
	{
		// Ÿ�� ����, ��Ÿ� �̳�, ���ݰ��� : ����
		if (target != null && Vector3.Distance(transform.position.YZero(), target.position.YZero()) < characterInfo.distancedRange 
			&& characterInfo.attackClock > 0f)
		{
			characterInfo.attackClock = -characterInfo.attackSpeedAfter;
			attackCoroutine = StartCoroutine(AttackTarget());
		}
		// Ÿ�� ����, ��Ÿ� �̳�, ���� �Ұ��� : ���
		else if (target != null && Vector3.Distance(transform.position.YZero(), target.position.YZero()) < characterInfo.distancedRange
			&& characterInfo.attackClock <= 0f)
		{
			characterInfo.moveTarget = transform.position;
		}
		// Ÿ�� ����, ��Ÿ� ��, ���ݰ��� Ȥ�� �Ұ��� : �ִ� ��Ÿ����� �̵�
		else if (target != null && Vector3.Distance(transform.position.YZero(), target.position.YZero()) > characterInfo.distancedRange)
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

		//�������� �� �ٶ󺸱�
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

		var bullet = BoltNetwork.Instantiate(characterBullet);
		bullet.GetComponent<CharacterBullet>().Enable(target, transform.position.YZero() + new Vector3(0, 1, 0), characterInfo);
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
