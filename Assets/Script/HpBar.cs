using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Bolt;

public class HpBar : EntityBehaviour<IMinionState>
{
    public float fullHp;
    public float currentHp;
    public Image hpBarImage;
    public Renderer currentRenderer;

    protected float barHeight;

    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
        if (currentRenderer == null)
        {
            currentRenderer = transform.parent.parent.GetComponentInChildren<Renderer>();
        }
    }

    protected void Init()
    {
        GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // ü���� ��Ұ�, ȭ�鿡 ���϶��� ���
        if (currentHp < fullHp && currentHp > 0 && mainCamera.IsObjectVisible(currentRenderer))
        {
            GetComponent<Canvas>().enabled = true;
            hpBarImage.fillAmount = currentHp / fullHp;
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
            transform.position = transform.parent.position + transform.up * barHeight;
		}
		else
        {
            GetComponent<Canvas>().enabled = false;
        }
    }
}
