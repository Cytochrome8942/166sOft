using System.Collections;
using UnityEngine;
using UnityEditor;

public class CharacterControl : MonoBehaviour
{
	private CharacterMove characterMove;
	private CharacterAttack characterAttack;
	private CharacterSkill characterSkill;

	public CharacterInfo characterInfo;

	private int layermask = ~(1 << 2);

	void Awake()
	{
		characterInfo.Reset();

		characterInfo.moveTarget = transform.position.YZero();

		characterMove = GetComponent<CharacterMove>();
		characterMove.characterInfo = characterInfo;
		characterAttack = GetComponent<CharacterAttack>();
		characterAttack.characterInfo = characterInfo;
		characterSkill = GetComponent<CharacterSkill>();
		characterSkill.characterInfo = characterInfo;
		GetComponent<CharacterLevel>().characterInfo = characterInfo;
	}


	public void RightClickInput(Vector3 mousePosition)
	{
		// RaycastAll은 순서를 보장하지 않기 때문에 우선순위에 따라 따로 판정해야함

		Ray ray = Camera.main.ScreenPointToRay(mousePosition);

		Debug.DrawRay(ray.origin, ray.direction * 50, Color.red, 5f);


		for (int i = 0; i<5; i++)
		{
			// 뭔가에 부딪혔다면
			if (Physics.Raycast(ray, out RaycastHit hit, 50f, layermask))
			{
				if (hit.transform.CompareTag("Minion")) // 2순위
				{
					if (hit.transform.GetComponent<MinionControl>().IsEnemy(characterInfo.team))
					{
						characterAttack.AttackTargetSet(hit.transform);
						break;
					}
					else
					{
						ray = new Ray(hit.point, ray.direction);
						continue;
					}
				}
				else if (hit.transform.CompareTag("Player")) // 1순위
				{
					if (hit.transform.GetComponent<CharacterControl>().IsEnemy(characterInfo.team))
					{
						characterAttack.AttackTargetSet(hit.transform);
						break;
					}
					else
					{
						ray = new Ray(hit.point, ray.direction);
						continue;
					}
				}
				else if (hit.transform.CompareTag("Ground")) // 3순위
				{
					characterMove.MoveTo(hit);
					break;
				}
				else
				{
					ray = new Ray(hit.point, ray.direction);
					continue;
				}
			}
			else
			{
				break;
			}
		}
	}

	public void SkillRange(bool on)
	{

	}

	public void ExpGet(float amount)
	{
		characterInfo.exp.add(amount);
	}

	public void Damaged(float damage)
	{
		characterInfo.hp.set(characterInfo.hp.get() - damage);
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


	public bool IsEnemy(int target)
	{
		return characterInfo.team.IsEnemy(target);
	}
}
