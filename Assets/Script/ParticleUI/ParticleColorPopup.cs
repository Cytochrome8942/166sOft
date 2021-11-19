using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleColorPopup : MonoBehaviour
{
	private string changingName;
	private Image changingColorBar;
	private Image changingAlphaBar;

	private ParticleBuilder currentParticleBuilder;

	public Image settingColor;

	private Color currentColor;

	private int currentMode;

	public Text[] colorText;
	public Scrollbar[] scrollBars;

	public UIGradient[] uiGradients;
	public Image aBar;

	public void TurnOn(Image colorBar, Image alphaBar, string name, int mode, ParticleBuilder particleBuilder)
	{
		gameObject.SetActive(true);

		currentParticleBuilder = particleBuilder;

		changingName = name;
		changingColorBar = colorBar;
		changingAlphaBar = alphaBar;
		currentMode = mode;

		currentColor = changingColorBar.color.AddAlpha(alphaBar.fillAmount);
		//오버헤드 방지
		colorText[0].text = (currentColor.r * 255).ToString("N0");
		scrollBars[0].value = currentColor.r;
		colorText[1].text = (currentColor.g * 255).ToString("N0");
		scrollBars[1].value = currentColor.g;
		colorText[2].text = (currentColor.b * 255).ToString("N0");
		scrollBars[2].value = currentColor.b;
		colorText[3].text = (currentColor.a).ToString("N2");
		scrollBars[3].value = currentColor.a;

		SetValues();
	}

	private void SetValues()
	{
		settingColor.color = currentColor;
		changingColorBar.color = currentColor.DeleteAlpha();
		changingAlphaBar.fillAmount = currentColor.a;

		uiGradients[0].m_color1 = new Color(0f, currentColor.g, currentColor.b);
		uiGradients[0].m_color2 = new Color(1f, currentColor.g, currentColor.b);
		uiGradients[1].m_color1 = new Color(currentColor.r, 0f, currentColor.b);
		uiGradients[1].m_color2 = new Color(currentColor.r, 1f, currentColor.b);
		uiGradients[2].m_color1 = new Color(currentColor.r, currentColor.g, 0f);
		uiGradients[2].m_color2 = new Color(currentColor.r, currentColor.g, 1f);
		aBar.color = new Color(currentColor.r, currentColor.g, currentColor.b);



		if (currentMode == 0)  // Constant
		{
			currentParticleBuilder.SetConstant(currentColor, changingName);
		}
		else if (currentMode == 1)  // First
		{
			currentParticleBuilder.SetFirst(currentColor, changingName);
		}
		else // Second
		{
			currentParticleBuilder.SetSecond(currentColor, changingName);
		}
	}

	public void SetR(float value)
	{
		currentColor = new Color(value, currentColor.g, currentColor.b, currentColor.a);
		colorText[0].text = (currentColor.r * 255).ToString("N0");
		SetValues();
	}
	public void SetG(float value)
	{
		currentColor = new Color(currentColor.r, value, currentColor.b, currentColor.a);
		colorText[1].text = (currentColor.g * 255).ToString("N0");
		SetValues();
	}
	public void SetB(float value)
	{
		currentColor = new Color(currentColor.r, currentColor.g, value, currentColor.a);
		colorText[2].text = (currentColor.b * 255).ToString("N0");
		SetValues();
	}
	public void SetA(float value)
	{
		currentColor = new Color(currentColor.r, currentColor.g, currentColor.b, value);
		colorText[3].text = (currentColor.a).ToString("N2");
		SetValues();
	}
}
