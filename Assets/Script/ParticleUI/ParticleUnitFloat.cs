using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParticleUnitFloat : MonoBehaviour
{
    private ParticleBuilder particleBuilder;

    public float maxValue;
    public float initial;

    public GameObject scrollBar;
    public TextMeshProUGUI valueText;

    public GameObject scrollBar1;
    public TextMeshProUGUI valueText1;
    public GameObject scrollBar2;
    public TextMeshProUGUI valueText2;


    // Start is called before the first frame update
    void Awake()
    {
        particleBuilder = GetComponentInParent<ParticleBuilder>();

        particleBuilder.initializer.AddListener(Initialize);
        particleBuilder.loader.AddListener(LoadValue);
    }

    public void Initialize()
    {
        valueText1.text = (maxValue * initial).ToString("F2");
        scrollBar1.GetComponent<UnityEngine.UI.Scrollbar>().value = initial;
        valueText2.text = (maxValue * initial).ToString("F2");
        scrollBar2.GetComponent<UnityEngine.UI.Scrollbar>().value = initial;

        valueText.text = (maxValue * initial).ToString("F2");
        scrollBar.GetComponent<UnityEngine.UI.Scrollbar>().value = initial;

        SetValue(scrollBar.GetComponent<UnityEngine.UI.Scrollbar>().value);
    }

	public void LoadValue()
    {
        if (! particleBuilder.currentParticle.floatProperties.TryGetValue(name, out Particle.FloatProperty savedProperty))
		{
            Debug.Log("LoadValue Critical Error" + name);
		}
        //ChangeMode에서 SetValue 호출
        if (savedProperty.isConstant)
        {
            valueText.text = (savedProperty.constantFloat.constantMin).ToString("F2");
            scrollBar.GetComponent<UnityEngine.UI.Scrollbar>().SetValueWithoutNotify(savedProperty.constantFloat.constantMin / maxValue);
            GetComponentInChildren<UnityEngine.UI.Dropdown>().value = 0;
			ChangeMode(0);
		}
		else
        {
            valueText1.text = (savedProperty.rangedFloat.constantMin).ToString("F2");
            scrollBar1.GetComponent<UnityEngine.UI.Scrollbar>().SetValueWithoutNotify(savedProperty.rangedFloat.constantMin / maxValue);
            valueText2.text = (savedProperty.rangedFloat.constantMax).ToString("F2");
            scrollBar2.GetComponent<UnityEngine.UI.Scrollbar>().SetValueWithoutNotify(savedProperty.rangedFloat.constantMax / maxValue);
            GetComponentInChildren<UnityEngine.UI.Dropdown>().value = 1;
            ChangeMode(1);
        }
	}

    public void SetValue(float value)
	{
        valueText.text = (maxValue * value).ToString("F2");
        particleBuilder.SetConstant(maxValue * value, name);
    }

    public void SetFirstValue(float value)
    {
        valueText1.text = (maxValue * value).ToString("F2");
        particleBuilder.SetFirst(maxValue * value, name);
    }

    public void SetSecondValue(float value)
    {
        valueText2.text = (maxValue * value).ToString("F2");
        particleBuilder.SetSecond(maxValue * value, name);
    }

    public void ChangeMode(int tick)
	{
        if(tick == 0)  // constant mode
        {
            scrollBar.SetActive(true);
            valueText.gameObject.SetActive(true);
            scrollBar1.SetActive(false);
            valueText1.gameObject.SetActive(false);
            scrollBar2.SetActive(false);
            valueText2.gameObject.SetActive(false);
            SetValue(scrollBar.GetComponent<UnityEngine.UI.Scrollbar>().value);
        }
		else if(tick == 1) // random mode
        {
            scrollBar.SetActive(false);
            valueText.gameObject.SetActive(false);
            scrollBar1.SetActive(true);
            valueText1.gameObject.SetActive(true);
            scrollBar2.SetActive(true);
            valueText2.gameObject.SetActive(true);

            SetFirstValue(scrollBar1.GetComponent<UnityEngine.UI.Scrollbar>().value);
            SetSecondValue(scrollBar2.GetComponent<UnityEngine.UI.Scrollbar>().value);
        }
	}
}
