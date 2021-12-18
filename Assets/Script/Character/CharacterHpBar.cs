using UnityEngine;
using UnityEngine.UI;
using Photon.Bolt;

public class CharacterHpBar : EntityBehaviour<IMinionState>
{
	public FloatData fullHp;
	public FloatData currentHp;
	public Image hpBarImage;

	private Camera mainCamera;

	void Awake()
	{
		mainCamera = Camera.main;
	}

	// Update is called once per frame
	public override void SimulateOwner()
	{
		// �׻� ���
		hpBarImage.fillAmount = state.Health / state.MaxHealth;
		transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
	}
}
