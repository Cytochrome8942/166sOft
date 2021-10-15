using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkill : MonoBehaviour
{
	public CharacterInfo characterInfo;

	//스킬
	public GameObject[] skillRange;
	public ParticleSystem[] skillEffect;

	[HideInInspector]
	public enum SKILLTYPE
	{
		TARGETLOCKED_ENEMY, // 타겟 고정 : 적군 
		TARGETLOCKED_ALLY,  // 타겟 고정 : 아군
		RANGED,  // 사거리
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
		
		characterMove.MoveLock(0.7f + 0.3f); // 시전시간 + 발동시간, 이동 및 회전 모두 금지
		yield return null;
		//TODO : 스킬 타입에 따라 Ground, Minion, Enemy 태그 확인 후 사용가능 여부 및 대상 고정 여부 판단
		var hit = GetHit(mousePosition);
		var tmpTarget = new Vector3(hit.point.x, 0, hit.point.z);
		Vector3 rotateTarget = (tmpTarget - transform.position).normalized;
		targetRotation = Quaternion.LookRotation(new Vector3(rotateTarget.x, 0, rotateTarget.z));

		transform.rotation = targetRotation;
		moveTarget = transform.position;
		// 스킬 파티클 사용
		yield return new WaitForSeconds(0.3f);
		Instantiate(skillEffect[skillNumber], skillEffect[skillNumber].transform.position, skillEffect[skillNumber].transform.rotation).Play();
		

	}*/
}
