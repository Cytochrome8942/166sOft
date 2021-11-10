using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ParticleUnitColor : MonoBehaviour
{
    private ParticleBuilder particleBuilder;
    private ParticleColorPopup particleColorPopup;

    public Image constantBar;
    public Image constantAlphaBar;
    public Image firstBar;
    public Image firstAlphaBar;
    public Image secondBar;
    public Image secondAlphaBar;

    private int currentMode = 0;

    // Start is called before the first frame update
    void Awake()
    {
        particleBuilder = GetComponentInParent<ParticleBuilder>();
        particleColorPopup = particleBuilder.particleColorPopup;

        particleBuilder.initializer.AddListener(Initialize);
        particleBuilder.loader.AddListener(LoadValue);
    }

    public void Initialize()
	{
        constantBar.color = Color.white;
        constantAlphaBar.fillAmount = 1;
        GetComponentInChildren<Dropdown>().value = 0;
        ChangeMode(0);
    }

    public void LoadValue()
    {
        if (!particleBuilder.currentParticle.colorProperties.TryGetValue(name, out Particle.ColorProperty savedProperty))
        {
            Debug.Log("LoadValue Critical Error" + name);
        }

        if (savedProperty.mode == 0)
        {
            constantBar.color = (savedProperty.constantColor.colorMin.DeleteAlpha());
            constantAlphaBar.fillAmount = (savedProperty.constantColor.colorMin.a);
            GetComponentInChildren<Dropdown>().value = 0;
            ChangeMode(0);
        }
        else if (savedProperty.mode == 1)
        {
            firstBar.color = (savedProperty.rangedColor.colorMin.DeleteAlpha());
            firstAlphaBar.fillAmount = (savedProperty.rangedColor.colorMin.a);
            secondBar.color = (savedProperty.rangedColor.colorMax.DeleteAlpha());
            secondAlphaBar.fillAmount = (savedProperty.rangedColor.colorMax.a);
            GetComponentInChildren<Dropdown>().value = 1;
            ChangeMode(1);
        }
		else
        {
            firstBar.color = (savedProperty.gradientColor.colorMin.DeleteAlpha());
            firstAlphaBar.fillAmount = (savedProperty.gradientColor.colorMin.a);
            secondBar.color = (savedProperty.gradientColor.colorMax.DeleteAlpha());
            secondAlphaBar.fillAmount = (savedProperty.gradientColor.colorMax.a);
            GetComponentInChildren<Dropdown>().value = 2;
            ChangeMode(2);
		}
    }

    public void CallConstant()
	{
        particleColorPopup.TurnOn(constantBar, constantAlphaBar, name, 0, particleBuilder);
	}

    public void CallFirst()
    {
        if (currentMode == 1)
        {
            particleColorPopup.TurnOn(firstBar, firstAlphaBar, name, 1, particleBuilder);
        }
		else // 2
        {
            particleColorPopup.TurnOn(firstBar, firstAlphaBar, "colorOverGradient", 1, particleBuilder);
        }
	}

    public void CallSecond()
    {
        if (currentMode == 1)
        {
            particleColorPopup.TurnOn(secondBar, secondAlphaBar, name, 2, particleBuilder);
        }
        else // 2
        {
            particleColorPopup.TurnOn(secondBar, secondAlphaBar, "colorOverGradient", 2, particleBuilder);
        }
    }

    public void ChangeMode(int tick)
    {
        if (tick == 0)  // constant mode
        {
            currentMode = 0;
            constantBar.gameObject.SetActive(true);
            constantAlphaBar.gameObject.SetActive(true);
            firstBar.gameObject.SetActive(false);
            firstAlphaBar.gameObject.SetActive(false);
            secondBar.gameObject.SetActive(false);
            secondAlphaBar.gameObject.SetActive(false);
            particleBuilder.SetConstant(constantBar.color.AddAlpha(constantAlphaBar.fillAmount), name);
        }
        else if(tick == 1) // random mode
        {
            currentMode = 1;
            constantBar.gameObject.SetActive(false);
            constantAlphaBar.gameObject.SetActive(false);
            firstBar.gameObject.SetActive(true);
            firstAlphaBar.gameObject.SetActive(true);
            secondBar.gameObject.SetActive(true);
            secondAlphaBar.gameObject.SetActive(true);

            particleBuilder.SetFirst(firstBar.color.AddAlpha(firstAlphaBar.fillAmount), name);
            particleBuilder.SetSecond(secondBar.color.AddAlpha(secondAlphaBar.fillAmount), name);
        }
		else // gradient mode
        {
            currentMode = 2;
            constantBar.gameObject.SetActive(false);
            constantAlphaBar.gameObject.SetActive(false);
            firstBar.gameObject.SetActive(true);
            firstAlphaBar.gameObject.SetActive(true);
            secondBar.gameObject.SetActive(true);
            secondAlphaBar.gameObject.SetActive(true);

            particleBuilder.SetFirst(firstBar.color.AddAlpha(firstAlphaBar.fillAmount), "colorOverGradient");
            particleBuilder.SetSecond(secondBar.color.AddAlpha(secondAlphaBar.fillAmount), "colorOverGradient");
        }
    }
}
