using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLoadButton : MonoBehaviour
{
	public UnityEngine.UI.Text myText;

    public void Clicked()
	{
		GetComponentInParent<ParticleLoadPopup>().LoadParticle(myText.text);
	}
}
