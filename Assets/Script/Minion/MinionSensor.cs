using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class MinionSensor : EnemySensor
{
	private MinionInfo minionInfo;

	public MinionMove minionMove;

	public void Initialize(MinionInfo minionInfo)
	{
		this.minionInfo = minionInfo;
		minionInfo.deathEvent.AddListener(Die);
		team = minionInfo.team;
		GetComponent<SphereCollider>().radius = minionInfo.targetRange;
	}

    public override void SimulateOwner()
	{
		if ((minionTargets.Count != 0 || characterTargets.Count != 0) && (minionInfo.target == null || minionInfo.target.CompareTag("Path")))
		{
			SetNearestTarget();
			minionInfo.target = target;
		}
		else if (minionTargets.Count == 0 && characterTargets.Count == 0 && minionInfo.target == null)
		{
			minionMove.SetNearestPath();
		}
	}
}
