using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class CharacterBullet : EntityBehaviour<IBulletState>
{
	private Transform target;

	public CharacterInfo characterInfo;

	public float bulletSpeed;
	public override void Attached(){
		state.SetTransforms(state.Position, transform);
	}

	public void Enable(Transform target, Vector3 firstPosition, CharacterInfo info)
	{
		transform.position = firstPosition;
		this.target = target;
		GetComponent<Collider>().enabled = true;
		GetComponent<MeshRenderer>().enabled = true;
		gameObject.SetActive(true);
		transform.SetAsLastSibling();
		characterInfo = info;
	}

	public override void SimulateOwner()
	{
		if (target != null)
		{
			transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * bulletSpeed);
		}
		else
		{
			StartCoroutine(Disable());
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(entity.IsOwner){
			if(other.CompareTag("Minion") && other.transform == target)
			{
				var hitEvent = bulletHitEvent.Create(other.gameObject.GetComponentInParent<BoltEntity>());
				hitEvent.Damage = characterInfo.physicalAttack.get();
				hitEvent.Send();
				StartCoroutine(Disable()); 
			}
			if(other.CompareTag("Enemy") && other.transform == target)
			{
				var hitEvent = bulletHitEvent.Create(other.gameObject.GetComponent<BoltEntity>());
				hitEvent.Damage = characterInfo.physicalAttack.get();
				hitEvent.Send();
				StartCoroutine(Disable());
			}
			if(other.CompareTag("Facility") && other.transform == target)
			{
				var hitEvent = bulletHitEvent.Create(other.gameObject.GetComponentInParent<BoltEntity>());
				/*
				if(other.TryGetComponent(out TowerControl towerControl))
				{
					// �������ݷ� > �������ݷ��� ��� ���� ��Ÿ
					float attack = characterInfo.physicalAttack.get() > characterInfo.magicalAttack.get() ?
						characterInfo.physicalAttack.get() : characterInfo.magicalAttack.get();
					towerControl.Damaged(attack, characterInfo.physicalAttack.get() > characterInfo.magicalAttack.get());
				}
				*/
				hitEvent.Damage = characterInfo.physicalAttack.get();
				hitEvent.Send();
				StartCoroutine(Disable());
			}
		}
	}

	private IEnumerator Disable()
	{
		target = null;
		GetComponent<Collider>().enabled = false;
		GetComponent<MeshRenderer>().enabled = false;
		yield return new WaitForSeconds(0.3f);
		gameObject.SetActive(false);
	}
}
