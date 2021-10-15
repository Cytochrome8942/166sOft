using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkill : MonoBehaviour
{
	public CharacterInfo characterInfo;

	//��ų
	public GameObject[] skillRange;
	public ParticleSystem[] skillEffect;

	[HideInInspector]
	public enum SKILLTYPE
	{
		TARGETLOCKED_ENEMY, // Ÿ�� ���� : ���� 
		TARGETLOCKED_ALLY,  // Ÿ�� ���� : �Ʊ�
		RANGED,  // ��Ÿ�
	}

	float skillUsableClock = 0f;


	public void SkillLock(float time)
	{
		skillUsableClock = skillUsableClock > -time ? -time : skillUsableClock;
	}

	public void SkillRange(bool on)
	{

	}
	/*
	public IEnumerator SkillShoot(Vector3 mousePosition, int skillNumber)
	{
		
		characterMove.MoveLock(0.7f + 0.3f); // �����ð� + �ߵ��ð�, �̵� �� ȸ�� ��� ����
		yield return null;
		//TODO : ��ų Ÿ�Կ� ���� Ground, Minion, Enemy �±� Ȯ�� �� ��밡�� ���� �� ��� ���� ���� �Ǵ�
		var hit = GetHit(mousePosition);
		var tmpTarget = new Vector3(hit.point.x, 0, hit.point.z);
		Vector3 rotateTarget = (tmpTarget - transform.position).normalized;
		targetRotation = Quaternion.LookRotation(new Vector3(rotateTarget.x, 0, rotateTarget.z));

		transform.rotation = targetRotation;
		moveTarget = transform.position;
		// ��ų ��ƼŬ ���
		yield return new WaitForSeconds(0.3f);
		Instantiate(skillEffect[skillNumber], skillEffect[skillNumber].transform.position, skillEffect[skillNumber].transform.rotation).Play();
		

	}*/
}
