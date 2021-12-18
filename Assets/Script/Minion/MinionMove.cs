using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Bolt;

public class MinionMove : EntityBehaviour<IMinionState>
{
	[System.NonSerialized]
	public GameObject[] path;

	[System.NonSerialized]
	public MinionInfo minionInfo;

	public SphereCollider rangeCollider;

	private int currentPath;

	private NavMeshAgent navMeshAgent;

	public override void Attached(){
		state.SetTransforms(state.Pos, transform.parent);
	}
    public override void SimulateOwner()
	{
		if(minionInfo.target != null)
		{
			if (minionInfo.target.CompareTag("Path"))
			{
				navMeshAgent.SetDestination(minionInfo.target.position.YZero());
				if (Vector3.Distance(minionInfo.target.position, transform.position) < 2f && currentPath < path.Length - 1)
				{
					currentPath++;
					minionInfo.target = path[currentPath].transform;
				}
			}
			else
			{
				if (Vector3.Distance(minionInfo.target.position, transform.position) < minionInfo.attackRange && !minionInfo.attacking)
				{
					minionInfo.attacking = true;
					navMeshAgent.SetDestination(transform.position.YZero());
				}
				else if(!minionInfo.attacking)
				{
					navMeshAgent.SetDestination(minionInfo.target.position.YZero());
				}
			}
		}
	}

	public void SetNearestPath()
	{
		int best = 0;
		float bestDistance = Mathf.Infinity;
		for(int i=0; i<path.Length; i++)
		{
			float tmpDist = Vector3.Distance(transform.position, path[i].transform.position);
			if (tmpDist < bestDistance)
			{
				bestDistance = tmpDist;
				best = i;
			}
		}
		// ���� ����� ����� ���� ��η� �̵���(�ڷ� ���ư��� ���� ����)
		currentPath = best + 1;
		minionInfo.target = path[currentPath].transform;
	}

	private void Die()
	{
		minionInfo.target = null;
		navMeshAgent.enabled = false;
	}

	public void Initialize(MinionInfo minionInfo)
	{
		this.minionInfo = minionInfo;
		navMeshAgent = GetComponentInParent<NavMeshAgent>();
		navMeshAgent.enabled = true;
		rangeCollider.radius = minionInfo.targetRange;
		minionInfo.target = path[1].transform;
		currentPath = 1;
		minionInfo.deathEvent.AddListener(Die);
	}
}
