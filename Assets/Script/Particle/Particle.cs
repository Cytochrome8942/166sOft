using System.Collections.Generic;
using UnityEngine;

public class Particle
{
	// *** max particles : 500 ***

	public string particleType;

	public string particleName = "New Particle";

	public Dictionary<string, FloatProperty> floatProperties = new Dictionary<string, FloatProperty>();
	public Dictionary<string, ColorProperty> colorProperties = new Dictionary<string, ColorProperty>();
	public Dictionary<string, PlainFloat> plainFloats = new Dictionary<string, PlainFloat>();

	//main
	public FloatProperty startLifetime = new FloatProperty();
	public FloatProperty startSpeed = new FloatProperty();
	public FloatProperty startSize = new FloatProperty();
	public FloatProperty startRotation = new FloatProperty();
	public FloatProperty gravityModifier = new FloatProperty();
	public ColorProperty startColor = new ColorProperty();

	public EmissionProperty emissionProperty = new EmissionProperty();

	//shape
	public PlainFloat angle = new PlainFloat();
	public PlainFloat radius = new PlainFloat();
	public PlainFloat radiusThickness = new PlainFloat();
	public ParticleSystemShapeMultiModeValue arcMode = new ParticleSystemShapeMultiModeValue();
	public PlainFloat arcSpeed = new PlainFloat();
	public PlainFloat arcSpread = new PlainFloat();

	//curve
	public ParticleSystem.MinMaxCurve velocityOverLifetimeLinear = new ParticleSystem.MinMaxCurve();
	public PlainFloat velocityOverLifetimeOrbital = new PlainFloat();
	public ParticleSystem.MinMaxCurve sizeOverLifetimeModule = new ParticleSystem.MinMaxCurve();
	public FloatProperty rotationOverLifetime = new FloatProperty();

	//Texture
	public Color32Data[] color32Datas;
	public int width = 0;
	public int height = 0;

	public string filePath;

	[System.Serializable]
	public class PlainFloat
	{
		public float value = 0;
	}

	[System.Serializable]
	public class FloatProperty
	{
		public bool isConstant = true;

		public ParticleSystem.MinMaxCurve constantFloat;

		public ParticleSystem.MinMaxCurve rangedFloat;
	}


	[System.Serializable]
	public class ColorProperty
	{
		public int mode = 0;

		public ParticleSystem.MinMaxGradient constantColor;

		public ParticleSystem.MinMaxGradient rangedColor;

		public ParticleSystem.MinMaxGradient gradientColor;
	}

	[System.Serializable]
	public class EmissionProperty
	{
		public int rateOverTimeMode = 0;
		public ParticleSystem.MinMaxCurve rateOverTime;
		public ParticleSystem.Burst rateOverTimeBurst;
	}

	[System.Serializable]
	public class Color32Data
	{
		public byte r, g, b, a;
		public Color32Data(Color32 source)
		{
			r = source.r;
			g = source.g;
			b = source.b;
			a = source.a;
		}
	}


	// ParticleBuilder Initialize에 필요한 함수들을 리스트에 등록해둠
	public void SaveProperties()
	{
		if (floatProperties.Count != 0)
		{
			return;
		}
		floatProperties.Add("startLifetime", startLifetime);
		floatProperties.Add("startSpeed", startSpeed);
		floatProperties.Add("startSize", startSize);
		floatProperties.Add("startRotation", startRotation);
		floatProperties.Add("gravityModifier", gravityModifier);
		colorProperties.Add("startColor", startColor);

		plainFloats.Add("angle", angle);
		plainFloats.Add("radius", radius);
		plainFloats.Add("radiusThickness", radiusThickness);
		plainFloats.Add("arcSpeed", arcSpeed);
		plainFloats.Add("arcSpread", arcSpread);

		plainFloats.Add("velocityOverLifetimeOrbital", velocityOverLifetimeOrbital);
		floatProperties.Add("rotationOverLifetime", rotationOverLifetime);
	}

	public void SaveTexture(Texture2D source)
	{
		Color32[] colors = source.GetPixels32();
		width = source.width;
		height = source.height;
		color32Datas = new Color32Data[colors.Length];
		for (int i = 0; i < colors.Length; i++)
		{
			color32Datas[i] = new Color32Data(colors[i]);
		}
	}

	public Texture2D LoadTexture()
	{
		Texture2D texture = new Texture2D(this.width, this.height);
		Color32[] pixels = new Color32[color32Datas.Length];
		for (int i = 0; i < pixels.Length; i++)
		{
			pixels[i] = new Color32(color32Datas[i].r, color32Datas[i].g, color32Datas[i].b, color32Datas[i].a);
		}
		texture.SetPixels32(pixels);
		texture.Apply();
		return texture;
	}
}
