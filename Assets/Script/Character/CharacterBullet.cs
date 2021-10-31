using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBullet : MonoBehaviour
{
	private Transform target;

	public CharacterInfo characterInfo;

	public float bulletSpeed;

	public void Enable(Transform target, Vector3 firstPosition)
	{
		transform.position = firstPosition;
		this.target = target;
		GetComponent<Collider>().enabled = true;
		GetComponent<MeshRenderer>().enabled = true;
		gameObject.SetActive(true);
		transform.SetAsLastSibling();
	}

	private void Update()
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
		if(other.CompareTag("Minion") && other.transform == target)
		{
			other.GetComponent<MinionControl>().Damaged(characterInfo.physicalAttack.get());
			StartCoroutine(Disable());
		}
		if(other.CompareTag("Enemy") && other.transform == target)
		{
			StartCoroutine(Disable());
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
