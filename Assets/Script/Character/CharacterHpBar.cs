using UnityEngine;
using UnityEngine.UI;
using Photon.Bolt;

public class CharacterHpBar : EntityBehaviour<IMinionState>
{
	public Image hpBarImage;

	private Camera mainCamera;

	public override void Attached()
	{
		mainCamera = Camera.main;
	}

	// Update is called once per frame
	public void Update()
	{
		// �׻� ���
		hpBarImage.fillAmount = GetComponentInParent<BoltEntity>().GetState<IMinionState>().Health 
		/ GetComponentInParent<BoltEntity>().GetState<IMinionState>().MaxHealth;
		transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
	}
}
