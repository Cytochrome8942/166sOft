using System.Collections;
using UnityEngine;
using UnityEditor;

public class CharacterControl : CommonObject
{
	private CharacterMove characterMove;
	private CharacterAttack characterAttack;
	public CharacterSkill[] characterSkill;

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
		for (int i = 0; i < characterSkill.Length; i++)
		{
			characterSkill[i].Initialize(characterInfo);
		}
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
				if (hit.transform.CompareTag("Minion"))
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
				else if (hit.transform.CompareTag("Player"))
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
				else if (hit.transform.CompareTag("Facility"))
				{
					if(hit.transform.TryGetComponent(out TowerControl towerControl))
					{
						if (towerControl.IsEnemy(characterInfo.team))
						{
							characterAttack.AttackTargetSet(hit.transform);
						}
					}
				}
				else if (hit.transform.CompareTag("Ground"))
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

	public void Damaged(float attack, bool isPhysical = true)
	{
		if (isPhysical)
		{
			characterInfo.hp.add(- attack.CalculateDamage(characterInfo.physicalDefence.get()));
		}
		else
		{
			characterInfo.hp.add(- attack.CalculateDamage(characterInfo.magicalDefence.get()));
		}
	}

	public void SkillShoot(Vector3 mousePosition, int skillNumber)
	{
		Ray ray = Camera.main.ScreenPointToRay(mousePosition);
		for (int i = 0; i < 5; i++)
		{
			// 뭔가에 부딪혔다면
			if (Physics.Raycast(ray, out RaycastHit hit, 50f, layermask))
			{
				if (hit.transform.CompareTag("Ground"))
				{
					characterSkill[skillNumber].Enable(hit.point.YZero());
					characterMove.MoveLock(characterSkill[skillNumber].skillInfo.beforeAttack + characterSkill[skillNumber].skillInfo.afterAttack);
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


	public bool IsEnemy(int target)
	{
		return characterInfo.team.IsEnemy(target);
	}
}
