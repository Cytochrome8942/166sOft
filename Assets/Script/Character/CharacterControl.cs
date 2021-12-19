using System.Collections;
using UnityEngine;
using UnityEditor;
using Photon.Bolt;
using System.Threading.Tasks;

public class CharacterControl : CommonObject
{
	private CharacterMove characterMove;
	private CharacterAttack characterAttack;
	public GameObject[] characterSkill;

	public CharacterInfo characterInfo;

	private int layermask = ~(1 << 2);


    public override void Attached()
	{
		characterInfo.Reset();
		characterInfo.team = (BoltGameInfo.isBlueTeam ? 1 : 0);
		state.Team = characterInfo.team;
		state.MaxHealth = characterInfo.fullHp.get();
		state.Health = characterInfo.fullHp.get();
		characterInfo.moveTarget = transform.position.YZero();

		characterMove = GetComponent<CharacterMove>();
		characterMove.characterInfo = characterInfo;
		characterAttack = GetComponent<CharacterAttack>();
		characterAttack.characterInfo = characterInfo;

		GetComponent<CharacterLevel>().characterInfo = characterInfo;
		GameObject.Find("GameManager").GetComponent<GameManager>().playerEntity = entity;
	}


	public void RightClickInput(Vector3 mousePosition)
	{
		// RaycastAll�� ������ �������� �ʱ� ������ �켱������ ���� ���� �����ؾ���

		Ray ray = Camera.main.ScreenPointToRay(mousePosition);

		Debug.DrawRay(ray.origin, ray.direction * 50, Color.red, 5f);


		for (int i = 0; i<5; i++)
		{
			// ������ �ε����ٸ�
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

	public async void Damaged(float attack, bool isPhysical = true)
	{
		if (isPhysical)
		{
			characterInfo.hp.add(- attack.CalculateDamage(characterInfo.physicalDefence.get()));
		}
		else
		{
			characterInfo.hp.add(- attack.CalculateDamage(characterInfo.magicalDefence.get()));
		}
		
		state.Health = characterInfo.hp.get();

		if(state.Health <= 0)
		{
			// 움직임 정지
			if(targetable)
			{
				targetable = false;
				characterMove.MoveLock(10);
				GetComponent<Animator>().SetTrigger("Die");

				await Task.Delay(9000);
				transform.position = ((state.Team == 0) ? new Vector3(-107, 0, 0) : new Vector3(107, 0, 0));
				GetComponent<Animator>().SetTrigger("Awake");

				characterInfo.hp.set(characterInfo.fullHp.get());
				Camera.main.GetComponent<CameraControl>().Reset();
				state.Health = characterInfo.hp.get();
				targetable = true;
			}
		}

	}
	public override void OnEvent(bulletHitEvent evnt){
		if(entity.IsOwner){
			Debug.LogWarning("player D");
			Damaged(evnt.Damage);
		}
	}
	public void SkillShoot(Vector3 mousePosition, int skillNumber)
	{
		Ray ray = Camera.main.ScreenPointToRay(mousePosition);
		for (int i = 0; i < 5; i++)
		{
			// ������ �ε����ٸ�
			if (Physics.Raycast(ray, out RaycastHit hit, 50f, layermask))
			{
				if (hit.transform.CompareTag("Ground"))
				{
				 	var newSkill = BoltNetwork.Instantiate(characterSkill[skillNumber], hit.point.YZero(), Quaternion.identity);

					float damage;
					var skillInfo = characterSkill[skillNumber].GetComponent<CharacterSkill>().skillInfo;
					if (skillInfo.isPhysical) {
						damage = skillInfo.damageRate * characterInfo.physicalAttack.get() + skillInfo.damage;
					}
					else
					{
						damage = skillInfo.damageRate * characterInfo.magicalAttack.get() + skillInfo.damage;
					}

					var skillevent = SkillEvent.Create(newSkill);
					skillevent.Damage = damage;
					skillevent.Team = state.Team;
					
					skillevent.Send();
					characterMove.MoveLock(skillInfo.beforeAttack + skillInfo.afterAttack);
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
		return state.Team.IsEnemy(target);
	}
}
