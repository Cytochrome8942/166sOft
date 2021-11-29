using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMove : MonoBehaviour
{
	[System.NonSerialized]
	public CharacterAttack characterAttack;

	Animator animator;

	public CharacterInfo characterInfo;

	//�̵�
	public GameObject moveParticleHolder;

	[System.NonSerialized]
	public NavMeshAgent navMeshAgent;

	void Awake()
	{
		animator = GetComponent<Animator>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		characterAttack = GetComponent<CharacterAttack>();
	}
	
	private void LateUpdate()
	{
		if (characterInfo.movableClock > 0f)
		{
			navMeshAgent.speed = characterInfo.moveSpeed;
		}
		else
		{
			navMeshAgent.speed = 0;
		}
		
		if (characterInfo.rotatableClock > 0f)
		{
			navMeshAgent.angularSpeed = characterInfo.rotateSpeed;
		}
		else
		{
			navMeshAgent.angularSpeed = 0;
		}

		navMeshAgent.SetDestination(characterInfo.moveTarget);

		if (navMeshAgent.velocity == Vector3.zero)
		{
			animator.SetBool("Move", false);
		}
		else
		{
			animator.SetBool("Move", true);
		}


		characterInfo.movableClock += Time.deltaTime;
		characterInfo.rotatableClock += Time.deltaTime;
	}
	
	public void MoveTo(RaycastHit hit)
	{
		characterAttack.CancelAttack();
		characterInfo.moveTarget = new Vector3(hit.point.x, 0, hit.point.z);

		//�ٴڿ� ����Ʈ
		Transform child = moveParticleHolder.transform.GetChild(0);
		child.position = characterInfo.moveTarget + new Vector3(0, 0.2f, 0);
		child.GetComponent<ParticleSystem>().Play();
		child.SetAsLastSibling();
	}


	public void MoveLock(float time, bool moveLock = true, bool rotateLock = true)
	{
		// clock �� �����Ϸ��� time���� Ŭ ��� time���� ��Ÿ�� ����, �̿ܿ��� ���� (�ߺ� Lock)
		if(moveLock && characterInfo.movableClock > -time)
		{
			characterInfo.movableClock = -time;
		}
		if(rotateLock && characterInfo.rotatableClock > -time)
		{
			characterInfo.rotatableClock = -time;
		}
	}
}
