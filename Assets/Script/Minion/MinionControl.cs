using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionControl : CommonObject
{
	private MinionInfo minionInfo;
	public MinionInfo setInfo;
    private cakeslice.Outline outline;
	private CapsuleCollider minionCollider;

	private MinionMove minionMove;

	public MinionSensor minionSensor;

	public MinionAttack minionAttack;

	Coroutine dieCoroutine;

	// �ð��� ���� ü�� �� ���ݷ� ��ȭ
	public void Initialize(GameObject[] path, int team)
	{
		minionInfo = Instantiate(setInfo);
		minionInfo.team = team;

		// *** N��

		GetComponentInChildren<MinionHpBar>().Initialize(minionInfo);

		outline = GetComponentInChildren<cakeslice.Outline>();
		minionCollider = GetComponent<CapsuleCollider>();

		minionMove = GetComponent<MinionMove>();
		minionMove.path = path;
		minionMove.Initialize(minionInfo);

		minionAttack.Initialize(minionInfo);

		minionSensor.Initialize(minionInfo);
	}

	public void Damaged(float attack, bool isPhysical = true)
	{
		if (isPhysical)
		{
			minionInfo.hp -= attack.CalculateDamage(minionInfo.physicalDefence);
		}
		else
		{
			minionInfo.hp -= attack.CalculateDamage(minionInfo.magicalDefence);
		}
		if (minionInfo.hp <= 0 && dieCoroutine == null)
		{
			targetable = false;
			dieCoroutine = StartCoroutine(Die());
		}
	}

	private IEnumerator Die()
	{
		// ��� �ִϸ��̼�
		minionInfo.deathEvent.Invoke();
		minionCollider.enabled = false;
		// ����ġ �й�
		GetComponent<ExpProvider>().ProvideExp(minionInfo.exp, minionInfo.team);

		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}

	public bool IsEnemy(int target)
	{
		return minionInfo.team.IsEnemy(target);
	}

	private void OnMouseOver()
	{
		if (targetable && GameManager.instance.playerInfo.team.IsEnemy(minionInfo.team))
		{
			outline.eraseRenderer = false;
		}
	}

	private void OnMouseExit()
	{
		outline.eraseRenderer = true;
	}
}
