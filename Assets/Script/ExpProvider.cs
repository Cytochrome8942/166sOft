using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpProvider : MonoBehaviour
{
	private const int PLAYER = 1 << 3;

	private const float expRange = 4;

	public void ProvideExp(float amount, int myTeam)
	{
		Collider[] targets = new Collider[10];
		int colliderAmount = Physics.OverlapSphereNonAlloc(transform.position.YZero(), expRange, targets, PLAYER, QueryTriggerInteraction.Collide);
		List<CharacterControl> enemies = new List<CharacterControl>();
		for (int i = 0; i < colliderAmount; i++)
		{
			var control = targets[i].transform.GetComponentInParent<CharacterControl>();
			// 적일때만 경험치 부여
			if (control.IsEnemy(myTeam))
			{
				enemies.Add(control);
			}
		}
		for (int i = 0; i < enemies.Count; i++)
		{
			enemies[i].ExpGet(amount / colliderAmount);
		}
	}
}
