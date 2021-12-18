using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Bolt;

public class CharacterMove : EntityBehaviour<IMinionState>
{
	[System.NonSerialized]
	public CharacterAttack characterAttack;

	Animator animator;

	public CharacterInfo characterInfo;

	//�̵�
	public GameObject moveParticleHolder;

	[System.NonSerialized]
	public NavMeshAgent navMeshAgent;

	public override void Attached()
	{
		animator = GetComponent<Animator>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		characterAttack = GetComponent<CharacterAttack>();
		state.SetAnimator(animator);
		Debug.LogWarning("aaa");

		state.SetTransforms(state.Pos, transform);
		if(entity.IsOwner){
			Debug.LogWarning("www");
			moveParticleHolder = GameObject.Find("GroundParticleHolder");
			GameObject.Find("InputController").GetComponent<InputControl>().characterControl = gameObject.GetComponent<CharacterControl>();
			GameObject.Find("GameManager").GetComponent<GameManager>().characterControl = gameObject.GetComponent<CharacterControl>();
			GameObject.Find("Main Camera").GetComponent<CameraControl>().character = gameObject;
			GameObject.Find("Main Camera").GetComponent<CameraControl>().init();
			GetComponent<CharacterLevel>().characterInfo = characterInfo;

			//characterInfo.team = state.isBlueTeam ? 1 : 0;
		}
	}
	
	public override void SimulateOwner()
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
			state.isMoving = false;
		}
		else
		{
			state.isMoving = true;
		}


		characterInfo.movableClock += Time.deltaTime;
		characterInfo.rotatableClock += Time.deltaTime;
	}

	public void Update(){
		animator.SetBool("Move", state.isMoving);
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
