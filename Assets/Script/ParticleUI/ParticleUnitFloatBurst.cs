using TMPro;
using UnityEngine;

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
		scrollBar.GetComponent<UnityEngine.UI.Scrollbar>().SetValueWithoutNotify(initial);
		valueText2.text = (maxValue * initial).ToString("F2");
		scrollBar.GetComponent<UnityEngine.UI.Scrollbar>().SetValueWithoutNotify(initial);

		countText.text = (maxBurstCount * initial).ToString("F2");
		burstCountScrollBar.GetComponent<UnityEngine.UI.Scrollbar>().SetValueWithoutNotify(initial);
		timesText.text = (maxTimes * initial).ToString("F2");
		burstTimesScrollBar.GetComponent<UnityEngine.UI.Scrollbar>().SetValueWithoutNotify(initial);


		particleBuilder.SetConstant(maxValue * initial, name);
		GetComponentInChildren<UnityEngine.UI.Dropdown>().value = 0;
	}

	public void LoadValue()
	{
		if (particleBuilder.currentParticle.emissionProperty.rateOverTimeMode == 0)
		{
			valueText.text = particleBuilder.currentParticle.emissionProperty.rateOverTime.constantMin.ToString("F2");
			scrollBar.GetComponent<UnityEngine.UI.Scrollbar>().SetValueWithoutNotify(particleBuilder.currentParticle.emissionProperty.rateOverTime.constantMin / maxValue);
		}
		else if (particleBuilder.currentParticle.emissionProperty.rateOverTimeMode == 1)
		{
			valueText1.text = particleBuilder.currentParticle.emissionProperty.rateOverTime.constantMin.ToString("F2");
			scrollBar1.GetComponent<UnityEngine.UI.Scrollbar>().SetValueWithoutNotify(particleBuilder.currentParticle.emissionProperty.rateOverTime.constantMin / maxValue);
			valueText2.text = particleBuilder.currentParticle.emissionProperty.rateOverTime.constantMax.ToString("F2");
			scrollBar2.GetComponent<UnityEngine.UI.Scrollbar>().SetValueWithoutNotify(particleBuilder.currentParticle.emissionProperty.rateOverTime.constantMax / maxValue);
		}
		else
		{
			countText.text = particleBuilder.currentParticle.emissionProperty.rateOverTimeBurst.count.constantMin.ToString("F2");
			burstCountScrollBar.GetComponent<UnityEngine.UI.Scrollbar>().SetValueWithoutNotify(particleBuilder.currentParticle.emissionProperty.rateOverTimeBurst.count.constantMin / maxBurstCount);
			timesText.text = particleBuilder.currentParticle.emissionProperty.rateOverTimeBurst.repeatInterval.ToString("F2");
			burstTimesScrollBar.GetComponent<UnityEngine.UI.Scrollbar>().SetValueWithoutNotify(particleBuilder.currentParticle.emissionProperty.rateOverTimeBurst.cycleCount / maxTimes);
		}
		GetComponentInChildren<UnityEngine.UI.Dropdown>().SetValueWithoutNotify(particleBuilder.currentParticle.emissionProperty.rateOverTimeMode);
		ChangeMode(particleBuilder.currentParticle.emissionProperty.rateOverTimeMode);
	}

	public void SetValue(float value)
	{
		valueText.text = (maxValue * value).ToString("F2");
		particleBuilder.SetConstant((maxValue * value), name);
	}

	public void SetFirstValue(float value)
	{
		valueText1.text = (maxValue * value).ToString("F2");
		particleBuilder.SetFirst((maxValue * value), name);
	}

	public void SetSecondValue(float value)
	{
		valueText2.text = (maxValue * value).ToString("F2");
		particleBuilder.SetSecond((maxValue * value), name);
	}

	public void SetBurstCount(float value)
	{
		countText.text = (maxBurstCount * value).ToString("F2");
		particleBuilder.SetFirst((maxBurstCount * value), name + "Burst");
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
