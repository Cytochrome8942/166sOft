using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    protected float fullHp;
    protected float currentHp;
    public Image hpBarImage;
    private MeshRenderer[] enemyRenderer;

    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
        enemyRenderer = transform.parent.GetComponentsInChildren<MeshRenderer>();
    }

    protected void Init()
    {
        GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // 체력이 닳았고, 화면에 보일때만 출력
        if (currentHp < fullHp && currentHp > 0 && mainCamera.IsObjectVisible(enemyRenderer[0]))
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
