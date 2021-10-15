using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParticleUnitFloatPlain : MonoBehaviour
{
    private ParticleBuilder particleBuilder;

    public float maxValue = 2;
    public float initial = 0.5f;

    public GameObject scrollBar;
    public TextMeshProUGUI valueText;


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
        particleBuilder.SetConstant(maxValue * initial, name);
    }

    public void LoadValue()
    {
        if (!particleBuilder.currentParticle.plainFloats.TryGetValue(name, out Particle.PlainFloat savedFloat))
        {
            Debug.Log("LoadValue Critical Error" + name);
        }

        valueText.text = (savedFloat.value).ToString("F2");
        scrollBar.GetComponent<UnityEngine.UI.Scrollbar>().value = savedFloat.value / maxValue;
        particleBuilder.SetConstant(savedFloat.value, name);
    }

    public void SetValue(float value)
    {
        valueText.text = (maxValue * value).ToString("F2");
        particleBuilder.SetConstant(maxValue * value, name);
    }
}
