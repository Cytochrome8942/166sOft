using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleCurvePopup : MonoBehaviour
{
	private string changingName;

	private AnimationCurve animationCurve;
	private ParticleBuilder currentParticleBuilder;

	public void TurnOn(string name, ParticleBuilder particleBuilder)
	{
		gameObject.SetActive(true);
		currentParticleBuilder = particleBuilder;
		changingName = name;
	}

	public void Off()
	{
		animationCurve = new AnimationCurve();
		animationCurve.AddKey(0.0f, 1.0f);
		animationCurve.AddKey(1.0f, 1.0f);
		currentParticleBuilder.SetCurve(new ParticleSystem.MinMaxCurve(1.0f, animationCurve), changingName);
	}

	public void Ascend()
	{
		animationCurve = new AnimationCurve();
		animationCurve.AddKey(0.0f, 0.0f);
		animationCurve.AddKey(1.0f, 1.0f);
		currentParticleBuilder.SetCurve(new ParticleSystem.MinMaxCurve(1.0f, animationCurve), changingName);
	}

	public void Descend()
	{
		animationCurve = new AnimationCurve();
		animationCurve.AddKey(0.0f, 1.0f);
		animationCurve.AddKey(1.0f, 0.0f);
		currentParticleBuilder.SetCurve(new ParticleSystem.MinMaxCurve(1.0f, animationCurve), changingName);
	}

	public void AscendSharp()
	{
		animationCurve = new AnimationCurve();
		animationCurve.AddKey(0.0f, 0.0f);
		animationCurve.AddKey(1.0f, 1.0f);
		Keyframe key1 = animationCurve[0];
		key1.outTangent = 2f;
		animationCurve.MoveKey(0, key1);
		Keyframe key2 = animationCurve[1];
		key2.inTangent = 0f;
		animationCurve.MoveKey(1, key2);
		currentParticleBuilder.SetCurve(new ParticleSystem.MinMaxCurve(1.0f, animationCurve), changingName);
	}
	
	public void DescendSharp()
	{
		animationCurve = new AnimationCurve();
		animationCurve.AddKey(0.0f, 1.0f);
		animationCurve.AddKey(1.0f, 0.0f);
		Keyframe key1 = animationCurve[0];
		key1.outTangent = 0f;
		animationCurve.MoveKey(0, key1);
		Keyframe key2 = animationCurve[1];
		key2.inTangent = -2f;
		animationCurve.MoveKey(1, key2);
		currentParticleBuilder.SetCurve(new ParticleSystem.MinMaxCurve(1.0f, animationCurve), changingName);
	}

	public void AscendSmooth()
	{
		animationCurve = new AnimationCurve();
		animationCurve.AddKey(0.0f, 0.0f);
		animationCurve.AddKey(1.0f, 1.0f);
		Keyframe key1 = animationCurve[0];
		key1.outTangent = 0f;
		animationCurve.MoveKey(0, key1);
		Keyframe key2 = animationCurve[1];
		key2.inTangent = 2f;
		animationCurve.MoveKey(1, key2);
		currentParticleBuilder.SetCurve(new ParticleSystem.MinMaxCurve(1.0f, animationCurve), changingName);
	}
	
	public void DescendSmooth()
	{
		animationCurve = new AnimationCurve();
		animationCurve.AddKey(0.0f, 1.0f);
		animationCurve.AddKey(1.0f, 0.0f);
		Keyframe key1 = animationCurve[0];
		key1.outTangent = -2f;
		animationCurve.MoveKey(0, key1);
		Keyframe key2 = animationCurve[1];
		key2.inTangent = 0f;
		animationCurve.MoveKey(1, key2);
		currentParticleBuilder.SetCurve(new ParticleSystem.MinMaxCurve(1.0f, animationCurve), changingName);
	}

	public void AscendSigmoid()
	{
		animationCurve = new AnimationCurve();
		animationCurve.AddKey(0.0f, 0.0f);
		animationCurve.AddKey(1.0f, 1.0f);
		Keyframe key1 = animationCurve[0];
		key1.outTangent = 0f;
		animationCurve.MoveKey(0, key1);
		Keyframe key2 = animationCurve[1];
		key2.inTangent = 0f;
		animationCurve.MoveKey(1, key2);
		currentParticleBuilder.SetCurve(new ParticleSystem.MinMaxCurve(1.0f, animationCurve), changingName);
	}
	
	public void DescendSigmoid()
	{
		animationCurve = new AnimationCurve();
		animationCurve.AddKey(0.0f, 1.0f);
		animationCurve.AddKey(1.0f, 0.0f);
		Keyframe key1 = animationCurve[0];
		key1.outTangent = 0f;
		animationCurve.MoveKey(0, key1);
		Keyframe key2 = animationCurve[1];
		key2.inTangent = 0f;
		animationCurve.MoveKey(1, key2);
		currentParticleBuilder.SetCurve(new ParticleSystem.MinMaxCurve(1.0f, animationCurve), changingName);
	}
}
