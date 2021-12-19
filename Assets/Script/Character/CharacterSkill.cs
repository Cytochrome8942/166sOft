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

	public override void OnEvent(SkillEvent s){
		Debug.LogWarning("wow Event");
		Enable(transform.position, s.Team, s.Damage);
	}
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

	public async void Enable(Vector3 targetPosition, int teamNum, float damage)
	{
		transform.position = targetPosition;
		beforeParticle.Play();
		rangeObject.SetActive(true);
		await Task.Delay((int)(skillInfo.beforeAttack * 1000));
		beforeParticle.Stop();
		activeParticle.Play();
		Collider[] targets = new Collider[20];
		int colliderAmount = Physics.OverlapSphereNonAlloc(targetPosition.YZero(), skillInfo.value1, targets, 1, QueryTriggerInteraction.Collide);

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
		rangeObject.SetActive(false);
		await Task.Delay((int)(skillInfo.afterAttack * 1000));
		activeParticle.Stop();
		finishParticle.Play();

		await Task.Delay(1000);
		finishParticle.Stop();
		BoltNetwork.Destroy(gameObject);
	}
}
