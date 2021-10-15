using System.Collections;
using UnityEngine;
using UnityEditor;

public class CharacterControl : MonoBehaviour
{
	private CharacterMove characterMove;
	private CharacterAttack characterAttack;
	private CharacterSkill characterSkill;

	public CharacterInfo characterInfo;

	void Awake()
	{
		characterInfo.moveTarget = transform.position.YZero();

		characterMove = GetComponent<CharacterMove>();
		characterMove.characterInfo = characterInfo;
		characterAttack = GetComponent<CharacterAttack>();
		characterAttack.characterInfo = characterInfo;
		characterSkill = GetComponent<CharacterSkill>();
		characterSkill.characterInfo = characterInfo;
	}


	public void RightClickInput(Vector3 mousePosition)
	{
		var hit = GetHit(mousePosition);

		if (hit.transform.CompareTag("Ground"))
		{
			characterMove.MoveTo(hit);
		}
		if (hit.transform.CompareTag("Minion") || hit.transform.CompareTag("Enemy"))
		{
			characterAttack.AttackTargetSet(hit.transform);
		}
	}

	public void SkillRange(bool on)
	{

	}

	public IEnumerator SkillShoot(Vector3 mousePosition, int skillNumber)
	{
		characterMove.MoveLock(0.7f + 0.3f); // 시전시간 + 발동시간, 이동 및 회전 모두 금지
		yield return null;
		/*
		//TODO : 스킬 타입에 따라 Ground, Minion, Enemy 태그 확인 후 사용가능 여부 및 대상 고정 여부 판단
		var hit = GetHit(mousePosition);
		var tmpTarget = new Vector3(hit.point.x, 0, hit.point.z);
		Vector3 rotateTarget = (tmpTarget - transform.position).normalized;
		targetRotation = Quaternion.LookRotation(new Vector3(rotateTarget.x, 0, rotateTarget.z));

		transform.rotation = targetRotation;
		moveTarget = transform.position;
		// 스킬 파티클 사용
		yield return new WaitForSeconds(0.3f);
		Instantiate(skillEffect[skillNumber], skillEffect[skillNumber].transform.position, skillEffect[skillNumber].transform.rotation).Play();*/
	}

	private RaycastHit GetHit(Vector3 mousePosition)
	{
		Ray ray = Camera.main.ScreenPointToRay(mousePosition);

		Debug.DrawRay(ray.origin, ray.direction * 50, Color.red, 5f);

		Physics.Raycast(ray, out RaycastHit hit, 50f);

		return hit;
	}
}
