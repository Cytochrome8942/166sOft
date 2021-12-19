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

    protected float barHeight;

    private Camera mainCamera;

    public override void Attached()
    {
        mainCamera = Camera.main;
        
    }
    protected void Init()
    {
        GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // ü���� ��Ұ�, ȭ�鿡 ���϶��� ���
        if (state.Health < state.MaxHealth && state.Health > 0)
        {
            GetComponent<Canvas>().enabled = true;
            hpBarImage.fillAmount = state.Health / state.MaxHealth;
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
            transform.position = transform.parent.position + transform.up * barHeight;
		}
		else
        {
            GetComponent<Canvas>().enabled = false;
        }
    }
}
