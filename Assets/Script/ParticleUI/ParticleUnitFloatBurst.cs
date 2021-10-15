using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParticleUnitFloatBurst : MonoBehaviour
{
    private ParticleBuilder particleBuilder;

    public float maxValue = 2;
    public float initial = 0.5f;

    public GameObject scrollBar;
    public TextMeshProUGUI valueText;

    public GameObject scrollBar1;
    public TextMeshProUGUI valueText1;
    public GameObject scrollBar2;
    public TextMeshProUGUI valueText2;

    public float maxBurstCount = 500;
    public GameObject burstCountScrollBar;
    public TextMeshProUGUI countText;

    public int maxTimes = 20;
    public GameObject burstTimesScrollBar;
    public TextMeshProUGUI timesText;


    // Start is called before the first frame update
    void Awake()
    {
        particleBuilder = GetComponentInParent<ParticleBuilder>();

        particleBuilder.initializer.AddListener(Initialize);
        particleBuilder.loader.AddListener(LoadValue);

    }

    public void Initialize()
    {
        valueText.text = (maxValue * initial).ToString("F2");
        scrollBar.GetComponent<UnityEngine.UI.Scrollbar>().value = initial;
        valueText1.text = (maxValue * initial).ToString("F2");
        scrollBar1.GetComponent<UnityEngine.UI.Scrollbar>().value = initial;
        valueText2.text = (maxValue * initial).ToString("F2");
        scrollBar2.GetComponent<UnityEngine.UI.Scrollbar>().value = initial;

        burstCountScrollBar.GetComponent<UnityEngine.UI.Scrollbar>().value = 0.2449f;
        countText.text = 30.ToString();
        burstTimesScrollBar.GetComponent<UnityEngine.UI.Scrollbar>().value = 0f;
        timesText.text = 1.ToString();

        particleBuilder.SetConstant(maxValue * Mathf.Pow(initial, 2), name);
        GetComponentInChildren<UnityEngine.UI.Dropdown>().value = 0;
    }

    // Reusable?
    public void LoadValue()
    {
        valueText.text = particleBuilder.currentParticle.rateOverTime.constantFloat.constantMin.ToString("F2");
        scrollBar.GetComponent<UnityEngine.UI.Scrollbar>().value = particleBuilder.currentParticle.rateOverTime.constantFloat.constantMin / maxValue;
        valueText1.text = particleBuilder.currentParticle.rateOverTime.rangedFloat.constantMin.ToString("F2");
        scrollBar1.GetComponent<UnityEngine.UI.Scrollbar>().value = particleBuilder.currentParticle.rateOverTime.rangedFloat.constantMin / maxValue;
        valueText2.text = particleBuilder.currentParticle.rateOverTime.rangedFloat.constantMax.ToString("F2");
        scrollBar2.GetComponent<UnityEngine.UI.Scrollbar>().value = particleBuilder.currentParticle.rateOverTime.rangedFloat.constantMax / maxValue;

        countText.text = particleBuilder.currentParticle.rateOverTimeBurst.constantFloat.constantMin.ToString("F2");
        burstCountScrollBar.GetComponent<UnityEngine.UI.Scrollbar>().value = particleBuilder.currentParticle.rateOverTimeBurst.constantFloat.constantMin / maxBurstCount;
        timesText.text = particleBuilder.currentParticle.rateOverTimeBurst.constantFloat.constantMax.ToString("F2");
        burstTimesScrollBar.GetComponent<UnityEngine.UI.Scrollbar>().value = particleBuilder.currentParticle.rateOverTimeBurst.constantFloat.constantMax / maxTimes;
        GetComponentInChildren<UnityEngine.UI.Dropdown>().value = particleBuilder.currentParticle.rateOverTimeMode;
        ChangeMode(particleBuilder.currentParticle.rateOverTimeMode);
    }

    public void SetValue(float value)
    {
        valueText.text = (maxValue * Mathf.Pow(value, 2)).ToString("F2");
        particleBuilder.SetConstant(maxValue * Mathf.Pow(value, 2), name);
    }

    public void SetFirstValue(float value)
    {
        valueText1.text = (maxValue * Mathf.Pow(value, 2)).ToString("F2");
        particleBuilder.SetFirst(maxValue * Mathf.Pow(value, 2), name);
    }

    public void SetSecondValue(float value)
    {
        valueText2.text = (maxValue * Mathf.Pow(value, 2)).ToString("F2");
        particleBuilder.SetSecond(maxValue * Mathf.Pow(value, 2), name);
    }

    public void SetBurstCount(float value)
	{
        countText.text = ((int)(maxBurstCount * Mathf.Pow(value, 2))).ToString();
        particleBuilder.SetFirst(((int)(maxBurstCount * Mathf.Pow(value, 2))), name + "Burst");
    }

    public void SetBurstTimes(float value)
	{
        timesText.text = (Mathf.RoundToInt(value * 19) + 1).ToString();
        particleBuilder.SetSecond((int)(Mathf.RoundToInt(value * 19) + 1), name + "Burst");
    }

    public void ChangeMode(int tick)
    {
        if (tick == 0)  // constant mode
        {
            scrollBar.SetActive(true);
            valueText.gameObject.SetActive(true);
            scrollBar1.SetActive(false);
            valueText1.gameObject.SetActive(false);
            scrollBar2.SetActive(false);
            valueText2.gameObject.SetActive(false);

            burstCountScrollBar.SetActive(false);
            countText.gameObject.SetActive(false);
            burstTimesScrollBar.SetActive(false);
            timesText.gameObject.SetActive(false);

            SetValue(scrollBar.GetComponent<UnityEngine.UI.Scrollbar>().value);
        }
        else if (tick == 1) // random mode
        {
            scrollBar.SetActive(false);
            valueText.gameObject.SetActive(false);
            scrollBar1.SetActive(true);
            valueText1.gameObject.SetActive(true);
            scrollBar2.SetActive(true);
            valueText2.gameObject.SetActive(true);

            burstCountScrollBar.SetActive(false);
            countText.gameObject.SetActive(false);
            burstTimesScrollBar.SetActive(false);
            timesText.gameObject.SetActive(false);

            SetFirstValue(scrollBar1.GetComponent<UnityEngine.UI.Scrollbar>().value);
            SetSecondValue(scrollBar2.GetComponent<UnityEngine.UI.Scrollbar>().value);
        }
        else if (tick == 2)
        {
            scrollBar.SetActive(false);
            valueText.gameObject.SetActive(false);
            scrollBar1.SetActive(false);
            valueText1.gameObject.SetActive(false);
            scrollBar2.SetActive(false);
            valueText2.gameObject.SetActive(false);

            burstCountScrollBar.SetActive(true);
            countText.gameObject.SetActive(true);
            burstTimesScrollBar.SetActive(true);
            timesText.gameObject.SetActive(true);

            SetFirstValue(burstCountScrollBar.GetComponent<UnityEngine.UI.Scrollbar>().value);
            SetSecondValue(burstTimesScrollBar.GetComponent<UnityEngine.UI.Scrollbar>().value);
        }
    }
}
