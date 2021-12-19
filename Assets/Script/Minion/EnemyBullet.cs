using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class EnemyBullet : EntityBehaviour<IBulletState>
{
	private Transform target;

	public float bulletSpeed;

	private float damage;
	public override void Attached(){
		state.SetTransforms(state.Position, transform);
	}
	public void Enable(Transform target, Vector3 firstPosition, float damage)
	{
		transform.position = firstPosition;
		this.target = target;
		GetComponent<Collider>().enabled = true;
		//GetComponent<MeshRenderer>().enabled = true;
		this.damage = damage;
		gameObject.SetActive(true);
		transform.SetAsLastSibling();
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
		if (other.CompareTag("Minion") && other.transform == target)
		{
			var e = bulletHitEvent.Create(target.GetComponentInParent<BoltEntity>());
			e.Damage = damage;
			e.Send();
			StartCoroutine(Disable());
		}
		if (other.CompareTag("Player") && other.transform == target)
		{
			var e = bulletHitEvent.Create(target.GetComponent<BoltEntity>());
			e.Damage = damage;
			e.Send(); 
			StartCoroutine(Disable());
		}
		if (other.CompareTag("Facility") && other.transform == target && other.transform != transform)
		{
			var e = bulletHitEvent.Create(target.GetComponentInParent<BoltEntity>());
			e.Damage = damage;
			e.Send();
			StartCoroutine(Disable());
		}
	}

	public override void Detached(){
		base.Detached();
	}

	private IEnumerator Disable()
	{
		//target = null;
		//GetComponent<Collider>().enabled = false;
		//GetComponent<MeshRenderer>().enabled = false;
		yield return new WaitForSeconds(0.1f);
		BoltEntity.Destroy(gameObject);
	}
}
