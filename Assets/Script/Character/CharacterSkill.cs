using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Photon.Bolt;

public class CharacterSkill : EntityEventListener<IBulletState>
{
	private CharacterInfo characterInfo;

	public GameObject rangeObject;
	public SkillInfo skillInfo;

	public ParticleSystem beforeParticle;
	public ParticleSystem activeParticle;
	public ParticleSystem finishParticle;

	public void Initialize(int teamNum, float damage)
	{
		if (skillInfo.skillType == SkillInfo.SkillType.Circle)
		{
			transform.localScale = new Vector3(skillInfo.value1, skillInfo.value1, skillInfo.value1);
		}
		rangeObject.transform.localScale = new Vector3(1, 0.001f, 1);
		
		Enable(teamNum, damage, true);
	}

	public async void Enable(int teamNum, float damage, bool isCanDamage)
	{
		Vector3 targetPosition = transform.position;
		beforeParticle.Play();
		rangeObject.SetActive(true);
		await Task.Delay((int)(skillInfo.beforeAttack * 1000));
		beforeParticle.Stop();
		activeParticle.Play();
		Collider[] targets = new Collider[20];
		int colliderAmount = Physics.OverlapSphereNonAlloc(targetPosition.YZero(), skillInfo.value1, targets, 1, QueryTriggerInteraction.Collide);

		if(isCanDamage){
			for (int i = 0; i < colliderAmount; i++)
			{
				if (targets[i].CompareTag("Minion"))
				{
					if (targets[i].GetComponent<MinionControl>().IsEnemy(teamNum))
					{
						var e = bulletHitEvent.Create(targets[i].GetComponentInParent<BoltEntity>());
						e.Damage = damage;
						e.Send();
					}
				}
				else if (targets[i].CompareTag("Player"))
				{
					if (targets[i].GetComponent<CharacterControl>().IsEnemy(teamNum))
					{
						var e = bulletHitEvent.Create(targets[i].GetComponent<BoltEntity>());
						e.Damage = damage;
						e.Send(); 
					}
				}
			}
		}

		rangeObject.SetActive(false);
		await Task.Delay((int)(skillInfo.afterAttack * 1000));
		activeParticle.Stop();
		finishParticle.Play();

		await Task.Delay(1000);
		finishParticle.Stop();
		Destroy(gameObject);
	}
}
