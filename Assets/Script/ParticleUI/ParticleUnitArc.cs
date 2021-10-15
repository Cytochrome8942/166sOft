using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParticleUnitArc : MonoBehaviour
{
    private ParticleBuilder particleBuilder;

    public float spreadMax = 1;
    public float spreadInit = 0f;

    public float speedMax = 5;
    public float speedInit = 0.2f;

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
        valueText.text = (spreadMax * spreadInit).ToString("F2");
        scrollBar.GetComponent<UnityEngine.UI.Scrollbar>().value = spreadInit;
        GetComponentInChildren<UnityEngine.UI.Dropdown>().value = 0;
        ChangeMode(0);
    }

    public void LoadValue()
    {
		switch (particleBuilder.currentParticle.arcMode)
		{
            case ParticleSystemShapeMultiModeValue.Random:
                valueText.text = particleBuilder.currentParticle.arcSpread.value.ToString("F2");
                scrollBar.GetComponent<UnityEngine.UI.Scrollbar>().value = particleBuilder.currentParticle.arcSpread.value;
                break;
            case ParticleSystemShapeMultiModeValue.Loop:
                valueText1.text = particleBuilder.currentParticle.arcSpread.value.ToString("F2");
                scrollBar1.GetComponent<UnityEngine.UI.Scrollbar>().value = particleBuilder.currentParticle.arcSpread.value;
                valueText2.text = particleBuilder.currentParticle.arcSpeed.value.ToString("F2");
                scrollBar2.GetComponent<UnityEngine.UI.Scrollbar>().value = particleBuilder.currentParticle.arcSpeed.value;
                break;
            case ParticleSystemShapeMultiModeValue.PingPong:
                valueText1.text = particleBuilder.currentParticle.arcSpread.value.ToString("F2");
                scrollBar1.GetComponent<UnityEngine.UI.Scrollbar>().value = particleBuilder.currentParticle.arcSpread.value;
                valueText2.text = particleBuilder.currentParticle.arcSpeed.value.ToString("F2");
                scrollBar2.GetComponent<UnityEngine.UI.Scrollbar>().value = particleBuilder.currentParticle.arcSpeed.value;
                break;
            case ParticleSystemShapeMultiModeValue.BurstSpread:
                valueText.text = particleBuilder.currentParticle.arcSpread.value.ToString("F2");
                scrollBar.GetComponent<UnityEngine.UI.Scrollbar>().value = particleBuilder.currentParticle.arcSpread.value;
                break;
        }
        GetComponentInChildren<UnityEngine.UI.Dropdown>().value = (int)particleBuilder.currentParticle.arcMode;
        ChangeMode((int)particleBuilder.currentParticle.arcMode);

    }

    public void SetSpreadAlone(float value)
    {
        valueText.text = (spreadMax * value).ToString("F2");
        particleBuilder.SetConstant(spreadMax * value, "arcSpread");
    }

    public void SetSpread(float value)
    {
        valueText1.text = (spreadMax * value).ToString("F2");
        particleBuilder.SetConstant(spreadMax * value, "arcSpread");
    }

    public void SetSpeed(float value)
    {
        valueText2.text = (speedMax * value).ToString("F2");
        particleBuilder.SetConstant(speedMax * value, "arcSpeed");
    }

    public void ChangeMode(int tick)
    {
        if (tick == 0)  // random mode
        {
            scrollBar.SetActive(true);
            valueText.gameObject.SetActive(true);
            scrollBar1.SetActive(false);
            valueText1.gameObject.SetActive(false);
            scrollBar2.SetActive(false);
            valueText2.gameObject.SetActive(false);
            particleBuilder.SetArcMode(ParticleSystemShapeMultiModeValue.Random);
            SetSpreadAlone(scrollBar.GetComponent<UnityEngine.UI.Scrollbar>().value);
        }
        else if (tick == 1) // PingPong mode
        {
            scrollBar.SetActive(false);
            valueText.gameObject.SetActive(false);
            scrollBar1.SetActive(true);
            valueText1.gameObject.SetActive(true);
            scrollBar2.SetActive(true);
            valueText2.gameObject.SetActive(true);

            particleBuilder.SetArcMode(ParticleSystemShapeMultiModeValue.PingPong);
            SetSpread(scrollBar1.GetComponent<UnityEngine.UI.Scrollbar>().value);
            SetSpeed(scrollBar2.GetComponent<UnityEngine.UI.Scrollbar>().value);
        }
        else if (tick == 2) // Loop mode
        {
            scrollBar.SetActive(false);
            valueText.gameObject.SetActive(false);
            scrollBar1.SetActive(true);
            valueText1.gameObject.SetActive(true);
            scrollBar2.SetActive(true);
            valueText2.gameObject.SetActive(true);

            particleBuilder.SetArcMode(ParticleSystemShapeMultiModeValue.Loop);
            SetSpread(scrollBar1.GetComponent<UnityEngine.UI.Scrollbar>().value);
            SetSpeed(scrollBar2.GetComponent<UnityEngine.UI.Scrollbar>().value);
        }
        else if (tick == 3) // BurstSpread mode
        {
            scrollBar.SetActive(true);
            valueText.gameObject.SetActive(true);
            scrollBar1.SetActive(false);
            valueText1.gameObject.SetActive(false);
            scrollBar2.SetActive(false);
            valueText2.gameObject.SetActive(false);
            particleBuilder.SetArcMode(ParticleSystemShapeMultiModeValue.BurstSpread);
            SetSpreadAlone(scrollBar.GetComponent<UnityEngine.UI.Scrollbar>().value);
        }
    }
}
