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
		characterInfo.moveTarget = transform.position.YZero();
		characterInfo.hp = characterInfo.fullHp;

		characterMove = GetComponent<CharacterMove>();
		characterMove.characterInfo = characterInfo;
		characterAttack = GetComponent<CharacterAttack>();
		characterAttack.characterInfo = characterInfo;
		characterSkill = GetComponent<CharacterSkill>();
		characterSkill.characterInfo = characterInfo;
	}


	public void RightClickInput(Vector3 mousePosition)
	{
		// RaycastAll�� ������ �������� �ʱ� ������ �켱������ ���� ���� �����ؾ���

		Ray ray = Camera.main.ScreenPointToRay(mousePosition);

		Debug.DrawRay(ray.origin, ray.direction * 50, Color.red, 5f);


		while (true)
		{
			// ������ �ε����ٸ�
			if (Physics.Raycast(ray, out RaycastHit hit, 50f, layermask))
			{
				if (hit.transform.CompareTag("Enemy"))
				{
					// �Ǿƽĺ�
					characterAttack.AttackTargetSet(hit.transform);
					break;
				}
				else if (hit.transform.CompareTag("Minion"))
				{
					//�Ǿƽĺ�
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
				else if (hit.transform.CompareTag("Ground"))
				{
					characterMove.MoveTo(hit);
					return;
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

	public void Damaged(float damage)
	{
		characterInfo.hp -= damage;
	}

	public IEnumerator SkillShoot(Vector3 mousePosition, int skillNumber)
	{
		characterMove.MoveLock(0.7f + 0.3f); // �����ð� + �ߵ��ð�, �̵� �� ȸ�� ��� ����
		yield return null;
		/*
		//TODO : ��ų Ÿ�Կ� ���� Ground, Minion, Enemy �±� Ȯ�� �� ��밡�� ���� �� ��� ���� ���� �Ǵ�
		var hit = GetHit(mousePosition);
		var tmpTarget = new Vector3(hit.point.x, 0, hit.point.z);
		Vector3 rotateTarget = (tmpTarget - transform.position).normalized;
		targetRotation = Quaternion.LookRotation(new Vector3(rotateTarget.x, 0, rotateTarget.z));

		transform.rotation = targetRotation;
		moveTarget = transform.position;
		// ��ų ��ƼŬ ���
		yield return new WaitForSeconds(0.3f);
		Instantiate(skillEffect[skillNumber], skillEffect[skillNumber].transform.position, skillEffect[skillNumber].transform.rotation).Play();*/
	}


	public bool IsEnemy(int target)
	{
		return characterInfo.team.IsEnemy(target);
	}
}
