using UnityEngine;
using UnityEngine.UI;

public class CharacterHpBar : MonoBehaviour
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
	protected virtual void Update()
	{
		// �׻� ���
		hpBarImage.fillAmount = currentHp.get() / fullHp.get();
		transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
	}
}
