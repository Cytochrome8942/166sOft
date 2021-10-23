using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionControl : MonoBehaviour
{
	private MinionInfo minionInfo;
	public MinionInfo setInfo;
    private cakeslice.Outline outline;
	private CapsuleCollider minionCollider;

	public MinionHpBar minionHpBar;

	private MinionMove minionMove;

	public MinionSensor minionSensor;

	public MinionAttack minionAttack;

	Coroutine dieCoroutine;

	private bool targetable = true;

	// 시간에 따른 체력 및 공격력 변화
	public void Initialize(GameObject[] path, int team)
	{
		minionInfo = Instantiate(setInfo);
		minionInfo.team = team;

		// *** N배

		minionHpBar.Initialize(minionInfo);

		outline = GetComponentInChildren<cakeslice.Outline>();
		minionCollider = GetComponent<CapsuleCollider>();

		minionMove = GetComponent<MinionMove>();
		minionMove.path = path;
		minionMove.Initialize(minionInfo);

		minionAttack.Initialize(minionInfo);

		minionSensor.Initialize(minionInfo);
	}

	public void Damaged(float damage)
	{
		minionInfo.hp -= damage;
		if (minionInfo.hp <= 0 && dieCoroutine == null)
		{
			dieCoroutine = StartCoroutine(Die());
		}
	}

	private IEnumerator Die()
	{
		// 사망 애니메이션
		minionInfo.deathEvent.Invoke();
		targetable = false;
		minionCollider.enabled = false;
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
