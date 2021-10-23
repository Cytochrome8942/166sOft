using System.Collections.Generic;
using UnityEngine;

public class MinionSensor : MonoBehaviour
{
	private MinionInfo minionInfo;

	public MinionMove minionMove;

	private List<Transform> targets = new List<Transform>();

	public void Initialize(MinionInfo minionInfo)
	{
		this.minionInfo = minionInfo;
		minionInfo.deathEvent.AddListener(Die);
		GetComponent<SphereCollider>().radius = minionInfo.targetRange;
	}

	public void Update()
	{
		if (targets.Count != 0 && (minionInfo.target == null || minionInfo.target.CompareTag("Path")))
		{
			SetNearestTarget();
		}
		else if (targets.Count == 0 && minionInfo.target == null)
		{
			minionMove.SetNearestPath();
		}
	}

	public void SetNearestTarget()
	{
		int targetMax = targets.Count;
		for (int i = 0; i < targetMax;)
		{
			if (targets[i] == null)
			{
				targets.RemoveAt(i);
				targetMax--;
				continue;
			}
			i++;
		}
		if (targets.Count > 0)
		{
			int best = 0;
			float bestDistance = Mathf.Infinity;
			for (int i = 0; i < targets.Count; i++)
			{
				float tmpDist = Vector3.Distance(transform.position, targets[i].position);
				if (tmpDist < bestDistance)
				{
					bestDistance = tmpDist;
					best = i;
				}
			}
			minionInfo.target = targets[best];
		}
	}

	private void Die()
	{
		transform.gameObject.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Minion"))
		{
			if (other.GetComponent<MinionControl>().IsEnemy(minionInfo.team))
			{
				targets.Add(other.transform);
			}
		}
		if (other.CompareTag("Player"))
		{
			if (other.GetComponent<CharacterControl>().IsEnemy(minionInfo.team))
			{
				targets.Add(other.transform);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (targets.Contains(other.transform))
		{
			targets.Remove(other.transform);
			if (minionInfo.target == other.transform)
			{
				minionInfo.target = null;
			}
		}
	}
}
