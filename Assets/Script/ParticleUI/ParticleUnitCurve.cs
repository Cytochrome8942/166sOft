using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleUnitCurve : MonoBehaviour
{
    private ParticleBuilder particleBuilder;
    private ParticleCurvePopup particleCurvePopup;


    // Start is called before the first frame update
    void Awake()
    {
        particleBuilder = GetComponentInParent<ParticleBuilder>();
        particleCurvePopup = particleBuilder.particleCurvePopup;

        particleBuilder.initializer.AddListener(Initialize);
        particleBuilder.loader.AddListener(LoadValue);
    }

    public void Initialize()
    {
        AnimationCurve animationCurve = new AnimationCurve();
        animationCurve.AddKey(0.0f, 1.0f);
        animationCurve.AddKey(1.0f, 1.0f);
        switch (name)
        {
            case "velocityOverLifetimeLinear":
                particleBuilder.SetCurve(new ParticleSystem.MinMaxCurve(1.0f, animationCurve), name);
                break;
            case "sizeOverLifetimeModule":
                particleBuilder.SetCurve(new ParticleSystem.MinMaxCurve(1.0f, animationCurve), name);
                break;
        }
    }

    public void LoadValue()
    {
		switch (name) {
            case "velocityOverLifetimeLinear":
                particleBuilder.SetCurve(particleBuilder.currentParticle.velocityOverLifetimeLinear, name);
                break;
            case "sizeOverLifetimeModule":
                particleBuilder.SetCurve(particleBuilder.currentParticle.sizeOverLifetimeModule, name);
                break;
        }
    }


    public void Clicked()
	{
        particleCurvePopup.TurnOn(name, particleBuilder);
	}
}
