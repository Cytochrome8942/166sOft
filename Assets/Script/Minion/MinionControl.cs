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

	private const int PLAYER = 1 << 3;

	Coroutine dieCoroutine;

	private bool targetable = true;

	// �ð��� ���� ü�� �� ���ݷ� ��ȭ
	public void Initialize(GameObject[] path, int team)
	{
		minionInfo = Instantiate(setInfo);
		minionInfo.team = team;

		// *** N��

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
		// ��� �ִϸ��̼�
		minionInfo.deathEvent.Invoke();
		targetable = false;
		minionCollider.enabled = false;
		// ����ġ �й�
		Collider[] targets = new Collider[10];
		int colliderAmount = Physics.OverlapSphereNonAlloc(transform.position.YZero(), minionInfo.expRange, targets, PLAYER, QueryTriggerInteraction.Collide);
		List<CharacterControl> enemies = new List<CharacterControl>();
		for(int i=0; i<colliderAmount; i++)
		{
			var control = targets[i].transform.GetComponentInParent<CharacterControl>();
			// ���϶��� ����ġ �ο�
			if (control.IsEnemy(minionInfo.team))
			{
				enemies.Add(control);
			}
		}
		for(int i=0; i<enemies.Count; i++)
		{
			enemies[i].ExpGet(minionInfo.exp / colliderAmount);
		}

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
