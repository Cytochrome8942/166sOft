using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionMove : MonoBehaviour
{
	[System.NonSerialized]
	public GameObject[] path;

	[System.NonSerialized]
	public MinionInfo minionInfo;

	public SphereCollider rangeCollider;

	private Transform target;

	private int currentPath;

	private NavMeshAgent navMeshAgent;

	private void Update()
	{
		if(target != null)
		{
			navMeshAgent.SetDestination(target.position.YZero());

			if(target.CompareTag("Path") && Vector3.Distance(target.position, transform.position) < 1f)
			{
				currentPath++;
				target = path[currentPath].transform;
			}
		}
	}

	public void Die()
	{
		target = null;
		navMeshAgent.enabled = false;
	}

	public void Initialize(MinionInfo minionInfo)
	{
		this.minionInfo = minionInfo;
		navMeshAgent = GetComponent<NavMeshAgent>();
		navMeshAgent.enabled = true;
		rangeCollider.radius = minionInfo.targetRange;
		target = path[1].transform;
		currentPath = 1;
	}
}
