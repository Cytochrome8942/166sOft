using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public float fullHp;
    public float currentHp;
    public Image hpBarImage;
    private MeshRenderer currentRenderer;

    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
        currentRenderer = transform.parent.GetComponentInChildren<MeshRenderer>();
    }

    protected void Init()
    {
        GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // 체력이 닳았고, 화면에 보일때만 출력
        if (currentHp < fullHp && currentHp > 0 && mainCamera.IsObjectVisible(currentRenderer))
        {
            GetComponent<Canvas>().enabled = true;
            hpBarImage.fillAmount = currentHp / fullHp;
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
            transform.position = transform.parent.position + transform.up * 1f;
		}
		else
        {
            GetComponent<Canvas>().enabled = false;
        }
    }
}
