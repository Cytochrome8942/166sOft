using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ParticleBuilder : MonoBehaviour
{
    public ParticleSystem targetParticle;

    public Particle currentParticle;

    public TMP_InputField particleNameHolder;

    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.EmissionModule emissionModule;
    private ParticleSystem.ShapeModule shapeModule;
    private ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule;
    private ParticleSystem.SizeOverLifetimeModule sizeOverLifetimeModule;
    private ParticleSystem.RotationOverLifetimeModule rotationOverLifetimeModule;
    private ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule;

    public ParticleColorPopup particleColorPopup;
    public ParticleCurvePopup particleCurvePopup;

    public string target;

    // buildScene 에서 True, game 초기화 중엔 False
    public bool isSaving = true;

    [System.NonSerialized]
    public UnityEvent loader = new UnityEvent();
    [System.NonSerialized]
    public UnityEvent initializer = new UnityEvent();

	private void Awake()
	{
        mainModule = targetParticle.main;
        emissionModule = targetParticle.emission;
        shapeModule = targetParticle.shape;
        velocityOverLifetimeModule = targetParticle.velocityOverLifetime;
        sizeOverLifetimeModule = targetParticle.sizeOverLifetime;
        rotationOverLifetimeModule = targetParticle.rotationOverLifetime;
        colorOverLifetimeModule = targetParticle.colorOverLifetime;
    }

    public void BuildAll(Particle particle)
	{
        foreach(KeyValuePair<string, Particle.FloatProperty> pair in particle.floatProperties)
		{
			if (pair.Value.isConstant)
			{
                SetConstant(pair.Value.constantFloat.constantMin, pair.Key);
			}
			else
			{
                SetFirst(pair.Value.rangedFloat.constantMin, pair.Key);
                SetSecond(pair.Value.rangedFloat.constantMax, pair.Key);
			}
		}

        foreach(KeyValuePair<string, Particle.ColorProperty> pair in particle.colorProperties)
		{
            if(pair.Value.mode == 0)
			{
                SetConstant(pair.Value.constantColor.colorMin, pair.Key);
			}
            else if(pair.Value.mode == 1)
			{
                SetFirst(pair.Value.rangedColor.colorMin, pair.Key);
                SetSecond(pair.Value.rangedColor.colorMax, pair.Key);
			}
			else
            {
                SetFirst(pair.Value.gradientColor.colorMin, pair.Key);
                SetSecond(pair.Value.gradientColor.colorMax, pair.Key);
            }
		}

        foreach(KeyValuePair<string, Particle.PlainFloat> pair in particle.plainFloats)
		{
            SetConstant(pair.Value.value, pair.Key);
		}

        //Emit / burst
        if(particle.emissionProperty.rateOverTimeMode == 0)
		{
            SetConstant(particle.emissionProperty.rateOverTime.constantMin, "rateOverTime");
		}
        else if (particle.emissionProperty.rateOverTimeMode == 1)
        {
            SetFirst(particle.emissionProperty.rateOverTime.constantMin, "rateOverTime");
            SetSecond(particle.emissionProperty.rateOverTime.constantMax, "rateOverTime");
        }
		else
        {
            SetFirst(particle.emissionProperty.rateOverTimeBurst.count.constantMin, "rateOverTime");
            SetSecond(particle.emissionProperty.rateOverTimeBurst.cycleCount, "rateOverTime");
        }

        SetCurve(particle.velocityOverLifetimeLinear, "velocityOverLifetimeLinear");
        SetCurve(particle.sizeOverLifetimeModule, "velocityOverLifetimeLinear");
        SetArcMode(particle.arcMode);
        SetTexture(particle.LoadTexture());
	}

    public void SetConstant(float value, string name)
	{
		switch (name)
		{
            case "startLifetime":
                mainModule.startLifetime = GetConstant(value);
                if (isSaving)
                {
                    SaveAsConstant(currentParticle.startLifetime, mainModule.startLifetime);
                }
                break;
            case "startSpeed":
                mainModule.startSpeed = GetConstant(value);
                if (isSaving)
                { 
                    SaveAsConstant(currentParticle.startSpeed, mainModule.startSpeed);
                }
                break;
            case "startSize":
                mainModule.startSize = GetConstant(value);
                if (isSaving)
                {
                    SaveAsConstant(currentParticle.startSize, mainModule.startSize);
                }
                break;
            case "startRotation":
                mainModule.startRotation = GetConstant(value);
                if (isSaving)
                {
                    SaveAsConstant(currentParticle.startRotation, mainModule.startRotation);
                }
                break;
            case "gravityModifier":
                mainModule.gravityModifier = GetConstant(value);
                if (isSaving)
                {
                    SaveAsConstant(currentParticle.gravityModifier, mainModule.gravityModifier);
                }
                break;
            case "rateOverTime":
                ParticleSystem.Burst tmpBurst = emissionModule.GetBurst(0);
                tmpBurst.count = 0;
                emissionModule.rateOverTime = GetConstant(value);
                if (isSaving)
                {
                    currentParticle.emissionProperty.rateOverTimeMode = 0;
                    currentParticle.emissionProperty.rateOverTime = emissionModule.rateOverTime;
                }
                break;
            case "angle":
                shapeModule.angle = value;
                if (isSaving)
                {
                    currentParticle.angle.value = value;
                }
                break;
            case "radius":
                shapeModule.radius = value;
                if (isSaving)
                {
                    currentParticle.radius.value = value;
                }
                break;
            case "radiusThickness":
                shapeModule.radiusThickness = value;
                if (isSaving)
                {
                    currentParticle.radiusThickness.value = value;
                }
                break;
            case "arcSpeed":
                shapeModule.arcSpeed = value;
                if (isSaving)
                {
                    currentParticle.arcSpeed.value = value;
                }
                break;
            case "arcSpread":
                shapeModule.arcSpread = value;
                if (isSaving)
                {
                    currentParticle.arcSpread.value = value;
                }
                break;
            case "velocityOverLifetimeOrbital":
                velocityOverLifetimeModule.orbitalY = value;
                if (isSaving)
                {
                    currentParticle.velocityOverLifetimeOrbital.value = value;
                }
                break;
            case "rotationOverLifetime":
                rotationOverLifetimeModule.z = GetConstant(value);
                if (isSaving)
                {
                    SaveAsConstant(currentParticle.rotationOverLifetime, rotationOverLifetimeModule.z);
                }
                break;
        }
	}

    public void SetConstant(Color value, string name)
	{
        switch (name)
		{
            case "startColor":
                mainModule.startColor = GetConstant(value);
                if (isSaving)
                {
                    SaveAsConstant(currentParticle.startColor, mainModule.startColor);
                }
                break;
		}
	}

    public void SetFirst(float value, string name)
    {
        switch (name)
        {
            case "startLifetime":
                mainModule.startLifetime = GetFirst(mainModule.startLifetime, value);
                if (isSaving)
                {
                    SaveAsRanged(currentParticle.startLifetime, mainModule.startLifetime);
                }
                break;
            case "startSpeed":
                mainModule.startSpeed = GetFirst(mainModule.startSpeed, value);
                if (isSaving)
                {
                    SaveAsRanged(currentParticle.startSpeed, mainModule.startSpeed);
                }
                break;
            case "startSize":
                mainModule.startSize = GetFirst(mainModule.startSize, value);
                if (isSaving)
                {
                    SaveAsRanged(currentParticle.startSize, mainModule.startSize);
                }
                break;
            case "startRotation":
                mainModule.startRotation = GetFirst(mainModule.startRotation, value);
                if (isSaving)
                {
                    SaveAsRanged(currentParticle.startRotation, mainModule.startRotation);
                }
                break;
            case "gravityModifier":
                mainModule.gravityModifier = GetFirst(mainModule.gravityModifier, value);
                if (isSaving)
                {
                    SaveAsRanged(currentParticle.gravityModifier, mainModule.gravityModifier);
                }
                break;
            case "rateOverTime":
                ParticleSystem.Burst tmpBurst = emissionModule.GetBurst(0);
                tmpBurst.count = 0;
                emissionModule.rateOverTime = GetFirst(emissionModule.rateOverTime, value);
                if (isSaving)
                {
                    currentParticle.emissionProperty.rateOverTimeMode = 1;
                    currentParticle.emissionProperty.rateOverTime = emissionModule.rateOverTime;
                }
                break;
            case "rateOverTimeBurst":
                currentParticle.emissionProperty.rateOverTimeMode = 2;
                ParticleSystem.Burst customBurst = emissionModule.GetBurst(0);
                customBurst.count = new ParticleSystem.MinMaxCurve(value, value);
                emissionModule.rateOverTime = 0;
                emissionModule.SetBurst(0, customBurst);
                if (isSaving)
                {
                    currentParticle.emissionProperty.rateOverTimeBurst = customBurst;
                }
                break;
            case "rotationOverLifetime":
                rotationOverLifetimeModule.z = GetFirst(rotationOverLifetimeModule.z, value);
                if (isSaving)
                {
                    SaveAsConstant(currentParticle.rotationOverLifetime, rotationOverLifetimeModule.z);
                }
                break;
        }
    }

    public void SetFirst(Color value, string name)
    {
        switch (name)
        {
            case "startColor":
                colorOverLifetimeModule.enabled = false;
                mainModule.startColor = GetFirst(mainModule.startColor, value);
                if (isSaving)
                {
                    SaveAsRanged(currentParticle.startColor, mainModule.startColor);
                }
                break;
            case "colorOverGradient":
                colorOverLifetimeModule.enabled = true;
                mainModule.startColor = Color.white;
                colorOverLifetimeModule.color = GetFirstGradient(colorOverLifetimeModule.color, value);
                if (isSaving)
                {
                    SaveAsGradient(currentParticle.startColor, colorOverLifetimeModule.color);
                }
                break;
        }
    }

    public void SetSecond(float value, string name)
    {
        switch (name)
        {
            case "startLifetime":
                mainModule.startLifetime = GetSecond(mainModule.startLifetime, value);
                if (isSaving)
                {
                    SaveAsRanged(currentParticle.startLifetime, mainModule.startLifetime);
                }
                break;
            case "startSpeed":
                mainModule.startSpeed = GetSecond(mainModule.startSpeed, value);
                if (isSaving)
                {
                    SaveAsRanged(currentParticle.startSpeed, mainModule.startSpeed);
                }
                break;
            case "startSize":
                mainModule.startSize = GetSecond(mainModule.startSize, value);
                if (isSaving)
                {
                    SaveAsRanged(currentParticle.startSize, mainModule.startSize);
                }
                break;
            case "startRotation":
                mainModule.startRotation = GetSecond(mainModule.startRotation, value);
                if (isSaving)
                {
                    SaveAsRanged(currentParticle.startRotation, mainModule.startRotation);
                }
                break;
            case "gravityModifier":
                mainModule.gravityModifier = GetSecond(mainModule.gravityModifier, value);
                if (isSaving)
                {
                    SaveAsRanged(currentParticle.gravityModifier, mainModule.gravityModifier);
                }
                break;
            case "rateOverTime":
                ParticleSystem.Burst tmpBurst = emissionModule.GetBurst(0);
                tmpBurst.count = 0;
                emissionModule.rateOverTime = GetSecond(emissionModule.rateOverTime, value);
                if (isSaving)
                {
                    currentParticle.emissionProperty.rateOverTimeMode = 1;
                    currentParticle.emissionProperty.rateOverTime = emissionModule.rateOverTime;
                }
                break;
            case "rateOverTimeBurst":
                currentParticle.emissionProperty.rateOverTimeMode = 2;
                ParticleSystem.Burst customBurst = emissionModule.GetBurst(0);
                customBurst.cycleCount = (int)value;
                customBurst.repeatInterval = 1 / value;
                emissionModule.burstCount = 1;
                emissionModule.SetBurst(0, customBurst);
                if (isSaving)
                {
                    currentParticle.emissionProperty.rateOverTimeBurst = customBurst;
                }
                break;
            case "rotationOverLifetime":
                rotationOverLifetimeModule.z = GetSecond(rotationOverLifetimeModule.z, value);
                if (isSaving)
                {
                    SaveAsConstant(currentParticle.rotationOverLifetime, rotationOverLifetimeModule.z);
                }
                break;
        }
    }

    public void SetSecond(Color value, string name)
    {
        switch (name)
        {
            case "startColor":
                colorOverLifetimeModule.enabled = false;
                mainModule.startColor = GetSecond(mainModule.startColor, value);
                if (isSaving)
                {
                    SaveAsRanged(currentParticle.startColor, mainModule.startColor);
                }
                break;
            case "colorOverGradient":
                colorOverLifetimeModule.enabled = true;
                mainModule.startColor = Color.white;
                colorOverLifetimeModule.color = GetSecondGradient(colorOverLifetimeModule.color, value);
                if (isSaving)
                {
                    SaveAsGradient(currentParticle.startColor, colorOverLifetimeModule.color);
                }
                break;
        }
    }

    public void SetArcMode(ParticleSystemShapeMultiModeValue mode)
	{
        shapeModule.arcMode = mode;
        if (isSaving)
        {
            currentParticle.arcMode = mode;
        }
	}

    public void SetCurve(ParticleSystem.MinMaxCurve animationCurve, string name)
	{
		switch (name)
		{
            case "velocityOverLifetimeLinear":
                velocityOverLifetimeModule.speedModifier= animationCurve;
                if (isSaving)
                {
                    currentParticle.velocityOverLifetimeLinear = animationCurve;
                }
                break;
            case "sizeOverLifetime":
                sizeOverLifetimeModule.size = animationCurve;
                if (isSaving)
                {
                    currentParticle.sizeOverLifetimeModule = animationCurve;
                }
                break;
        }
	}

    public void SetTexture(Texture2D texture)
	{
        Material particleMat = new Material(targetParticle.GetComponent<ParticleSystemRenderer>().material);
        particleMat.SetTexture("_MainTex", texture);
        targetParticle.GetComponent<ParticleSystemRenderer>().material = particleMat;
        if (isSaving)
        {
            currentParticle.SaveTexture(texture);
        }
    }

    // float
    private ParticleSystem.MinMaxCurve GetConstant(float value)
	{
        return new ParticleSystem.MinMaxCurve(value, value);
    }
    private ParticleSystem.MinMaxCurve GetFirst(ParticleSystem.MinMaxCurve curve, float value)
    {
        return new ParticleSystem.MinMaxCurve(value, curve.constantMax);
    }
    private ParticleSystem.MinMaxCurve GetSecond(ParticleSystem.MinMaxCurve curve, float value)
    {
        return new ParticleSystem.MinMaxCurve(curve.constantMin, value);
    }


    // color
    private ParticleSystem.MinMaxGradient GetConstant(Color value)
    {
        return new ParticleSystem.MinMaxGradient(value, value);
    }
    private ParticleSystem.MinMaxGradient GetFirst(ParticleSystem.MinMaxGradient curve, Color value)
    {
        return new ParticleSystem.MinMaxGradient(value, curve.colorMax);
    }
    private ParticleSystem.MinMaxGradient GetSecond(ParticleSystem.MinMaxGradient curve, Color value)
    {
        return new ParticleSystem.MinMaxGradient(curve.colorMin, value);
    }
    private ParticleSystem.MinMaxGradient GetFirstGradient(ParticleSystem.MinMaxGradient curve, Color value)
    {
        Gradient gradient = new Gradient();
        GradientColorKey[] newColorKeys = new GradientColorKey[2];
        GradientAlphaKey[] newAlphaKeys = new GradientAlphaKey[2];
        newColorKeys[0] = new GradientColorKey(value, 0f);
        newAlphaKeys[0] = new GradientAlphaKey(value.a, 0f);
        newColorKeys[1] = curve.gradient.colorKeys[1];
        newAlphaKeys[1] = curve.gradient.alphaKeys[1];
        gradient.colorKeys = newColorKeys;
        gradient.alphaKeys = newAlphaKeys;
        return new ParticleSystem.MinMaxGradient(gradient);
    }
    private ParticleSystem.MinMaxGradient GetSecondGradient(ParticleSystem.MinMaxGradient curve, Color value)
    {
        Gradient gradient = new Gradient();
        GradientColorKey[] newColorKeys = new GradientColorKey[2];
        GradientAlphaKey[] newAlphaKeys = new GradientAlphaKey[2];
        newColorKeys[0] = curve.gradient.colorKeys[0];
        newAlphaKeys[0] = curve.gradient.alphaKeys[0];
        newColorKeys[1] = new GradientColorKey(value, 1f);
        newAlphaKeys[1] = new GradientAlphaKey(value.a, 1f);
        gradient.colorKeys = newColorKeys;
        gradient.alphaKeys = newAlphaKeys;
        return new ParticleSystem.MinMaxGradient(gradient);
    }


    private void SaveAsConstant(Particle.FloatProperty floatProperty, ParticleSystem.MinMaxCurve curve)
	{
        floatProperty.isConstant = true;
        floatProperty.constantFloat = curve;
	}

    private void SaveAsConstant(Particle.ColorProperty colorProperty, ParticleSystem.MinMaxGradient curve)
    {
        colorProperty.mode = 0;
        colorProperty.constantColor = curve;
    }

    private void SaveAsRanged(Particle.FloatProperty floatProperty, ParticleSystem.MinMaxCurve curve)
    {
        floatProperty.isConstant = false;
        floatProperty.rangedFloat = curve;
    }

    private void SaveAsRanged(Particle.ColorProperty colorProperty, ParticleSystem.MinMaxGradient curve)
    {
        colorProperty.mode = 1;
        colorProperty.rangedColor = curve;
    }

    private void SaveAsGradient(Particle.ColorProperty colorProperty, ParticleSystem.MinMaxGradient curve)
    {
        colorProperty.mode = 2;
        colorProperty.gradientColor = curve;
    }
}
