using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Photon.Bolt;

public class CharacterSkill : EntityBehaviour<IBulletState>
{
	private CharacterInfo characterInfo;

	public GameObject rangeObject;
	public SkillInfo skillInfo;

	public ParticleSystem beforeParticle;
	public ParticleSystem activeParticle;
	public ParticleSystem finishParticle;


	private void Awake()
	{
		if (skillInfo.skillType == SkillInfo.SkillType.Circle)
		{
			transform.localScale = new Vector3(skillInfo.value1, skillInfo.value1, skillInfo.value1);
		}
		rangeObject.transform.localScale = new Vector3(1, 0.001f, 1);
	}

	public void Initialize(CharacterInfo characterInfo)
	{
		this.characterInfo = characterInfo;
	}

	public async void Enable(Vector3 targetPosition)
	{
		transform.position = targetPosition;
		beforeParticle.Play();
		rangeObject.SetActive(true);
		await Task.Delay((int)(skillInfo.beforeAttack * 1000));
		beforeParticle.Stop();
		activeParticle.Play();
		Collider[] targets = new Collider[20];
		int colliderAmount = Physics.OverlapSphereNonAlloc(targetPosition.YZero(), skillInfo.value1, targets, 1, QueryTriggerInteraction.Collide);

		float damage;
		if (skillInfo.isPhysical) {
			damage = skillInfo.damageRate * characterInfo.physicalAttack.get() + skillInfo.damage;
		}
		else
		{
			damage = skillInfo.damageRate * characterInfo.magicalAttack.get() + skillInfo.damage;
		}
		for (int i = 0; i < colliderAmount; i++)
		{
			if (targets[i].CompareTag("Minion"))
			{
				if (targets[i].GetComponent<MinionControl>().IsEnemy(characterInfo.team))
				{
					var e = bulletHitEvent.Create(targets[i].GetComponentInParent<BoltEntity>());
					e.Damage = damage;
					e.Send();
				}
			}
			else if (targets[i].CompareTag("Player"))
			{
				if (targets[i].GetComponent<CharacterControl>().IsEnemy(characterInfo.team))
				{
					var e = bulletHitEvent.Create(targets[i].GetComponent<BoltEntity>());
					e.Damage = damage;
					e.Send(); 
				}
			}
		}
		rangeObject.SetActive(false);
		await Task.Delay((int)(skillInfo.afterAttack * 1000));
		activeParticle.Stop();
		finishParticle.Play();

		await Task.Delay(1000);
		finishParticle.Stop();
		BoltNetwork.Destroy(gameObject);
	}
}
