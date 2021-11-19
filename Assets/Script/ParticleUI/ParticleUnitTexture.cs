using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleUnitTexture : MonoBehaviour
{
    public ParticleIO particleIO;
    private ParticleBuilder particleBuilder;
    private RawImage image;
    public Texture2D defaultTexture;

    void Awake()
    {
        image = GetComponent<RawImage>();

        particleBuilder = GetComponentInParent<ParticleBuilder>();

        particleBuilder.initializer.AddListener(Initialize);
        particleBuilder.loader.AddListener(LoadValue);
    }

    public void Initialize()
    {
        particleBuilder.SetTexture(defaultTexture);
        image.texture = defaultTexture;
    }

    public void LoadValue()
	{
        Texture2D loadedTexture = particleBuilder.currentParticle.LoadTexture();
        particleBuilder.SetTexture(loadedTexture);
        image.texture = loadedTexture;
    }

    public void OpenTextureWindow()
    {
        Texture2D loadedTexture = particleIO.LoadTexture();
        if (loadedTexture != null)
        {
            particleBuilder.SetTexture(loadedTexture);
            image.texture = loadedTexture;
        }
    }

    public void ResetTexture()
    {
        particleBuilder.SetTexture(defaultTexture);
        image.texture = defaultTexture;
    }
}
