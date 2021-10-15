using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [System.NonSerialized]
    public CharacterInfo characterInfo;

	[System.NonSerialized]
	public CharacterMove characterMove;

    //���Ÿ� / �ٰŸ�?
    public GameObject characterBulletHolder;

	private Coroutine attackCoroutine;

	//����Ÿ��
	private Transform target;

	private void Awake()
	{
		characterMove = GetComponent<CharacterMove>();
	}

	private void Update()
	{
		// Ÿ�� ����, ��Ÿ� �̳�, ���ݰ��� : ����
		if (target != null && Vector3.Distance(transform.position.YZero(), target.position.YZero()) < characterInfo.attackRange 
			&& characterInfo.attackClock > 0f)
		{
			characterInfo.attackClock = -characterInfo.attackSpeed;
			attackCoroutine = StartCoroutine(AttackTarget());
		}
		// Ÿ�� ����, ��Ÿ� �̳�, ���� �Ұ��� : ���
		else if (target != null && Vector3.Distance(transform.position.YZero(), target.position.YZero()) < characterInfo.attackRange
			&& characterInfo.attackClock <= 0f)
		{
			characterInfo.moveTarget = transform.position;
		}
		// Ÿ�� ����, ��Ÿ� ��, ���ݰ��� Ȥ�� �Ұ��� : �ִ� ��Ÿ����� �̵�
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
