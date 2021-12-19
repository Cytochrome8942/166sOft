using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

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
		state.Team = minionInfo.team;
		state.MaxHealth = minionInfo.hp;

		// *** N��
		GetComponentInChildren<MinionHpBar>().Initialize();

		minionCollider = GetComponent<CapsuleCollider>();

		minionMove = GetComponent<MinionMove>();
		minionMove.path = path;
		minionMove.Initialize(minionInfo);

		minionAttack.Initialize(minionInfo);

		minionSensor.Initialize(minionInfo);
	}
	public override void Attached(){
		outline = transform.parent.GetComponentInChildren<cakeslice.Outline>();
		GetComponentInChildren<MinionHpBar>().Initialize();
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
		state.Health = minionInfo.hp;
		if (minionInfo.hp <= 0 && dieCoroutine == null)
		{
			targetable = false;
			dieCoroutine = StartCoroutine(Die());
		}
	}

	public override void OnEvent(bulletHitEvent evnt){
		Debug.Log("minion D");
		if(entity.IsOwner){
			Damaged(evnt.Damage);
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
	    BoltNetwork.Destroy(gameObject.transform.parent.gameObject);
	}

	public bool IsEnemy(int target)
	{
		return state.Team.IsEnemy(target);
	}

	private void OnMouseOver()
	{
		if (!(targetable && GameManager.instance.playerEntity.GetState<IMinionState>().Team.IsEnemy(state.Team)))
		{
			outline.eraseRenderer = false;
		}
	}

	private void OnMouseExit()
	{
 			outline.eraseRenderer = true;
	}
}
