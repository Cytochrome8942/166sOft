using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
	protected List<Transform> minionTargets = new List<Transform>();
	protected List<Transform> characterTargets = new List<Transform>();

	// initialize
	protected int team = -1;
	protected Transform target;

	public void SetNearestTarget()
	{
		int targetMax = minionTargets.Count;
		for (int i = 0; i < targetMax;)
		{
			if (minionTargets[i] == null || !minionTargets[i].GetComponent<CommonObject>().targetable)
			{
				minionTargets.RemoveAt(i);
				targetMax--;
				continue;
			}
			i++;
		}
		targetMax = characterTargets.Count;
		for (int i = 0; i < targetMax;)
		{
			if (!characterTargets[i].GetComponent<CommonObject>().targetable)
			{
				characterTargets.RemoveAt(i);
				targetMax--;
				continue;
			}
			i++;
		}

		if (minionTargets.Count > 0)
		{
			int best = 0;
			float bestDistance = Mathf.Infinity;
			for (int i = 0; i < minionTargets.Count; i++)
			{
				float tmpDist = Vector3.Distance(transform.position, minionTargets[i].position);
				if (tmpDist < bestDistance)
				{
					bestDistance = tmpDist;
					best = i;
				}
			}
			target = minionTargets[best];
			return;
		}
		if(characterTargets.Count > 0)
		{
			int best = 0;
			float bestDistance = Mathf.Infinity;
			for (int i = 0; i < characterTargets.Count; i++)
			{
				float tmpDist = Vector3.Distance(transform.position, characterTargets[i].position);
				if (tmpDist < bestDistance)
				{
					bestDistance = tmpDist;
					best = i;
				}
			}
			target = characterTargets[best];
			return;
		}
		target = null;
		return;
	}

	protected void Die()
	{
		transform.gameObject.SetActive(false);
	}

	protected void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Minion"))
		{
			if (other.GetComponent<MinionControl>().IsEnemy(team))
			{
				minionTargets.Add(other.transform);
			}
		}
		if (other.CompareTag("Player"))
		{
			if (other.GetComponent<CharacterControl>().IsEnemy(team))
			{
				characterTargets.Add(other.transform);
			}
		}
		if (other.CompareTag("Facility"))
		{
			if(other.TryGetComponent(out TowerControl towerControl))
			{
				if (towerControl.IsEnemy(team))
				{
					minionTargets.Add(other.transform);
				}
			}
			// 넥서스 및 우물 추가
		}
	}

	protected virtual void OnTriggerExit(Collider other)
	{
		if (minionTargets.Contains(other.transform))
		{
			minionTargets.Remove(other.transform);
			if (target == other.transform)
			{
				target = null;
			}
		}
		else if (characterTargets.Contains(other.transform))
		{
			characterTargets.Remove(other.transform);
			if (target == other.transform)
			{
				target = null;
			}
		}
	}
}
