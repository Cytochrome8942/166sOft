using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionControl : MonoBehaviour
{
	private MinionInfo minionInfo;
    private cakeslice.Outline outline;
	private CapsuleCollider minionCollider;

	public MinionHpBar minionHpBar;

	private MinionMove minionMove;

	Coroutine dieCoroutine;

	// �ð��� ���� ü�� �� ���ݷ� ��ȭ
	public void Initialize(GameObject[] path)
	{
		minionInfo = ScriptableObject.CreateInstance<MinionInfo>();

		// *** N��

		minionHpBar.Initialize(minionInfo);

		outline = GetComponent<cakeslice.Outline>();
		minionCollider = GetComponent<CapsuleCollider>();
		minionMove = GetComponent<MinionMove>();
		minionMove.path = path;
		minionMove.Initialize(minionInfo);

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
		minionCollider.enabled = false;
		minionMove.Die();
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}

	private void OnMouseOver()
	{
        outline.eraseRenderer = false;
	}

	private void OnMouseExit()
	{
		outline.eraseRenderer = true;
	}
}
