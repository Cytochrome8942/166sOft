using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLoadPopup : MonoBehaviour
{
	public ParticleIO particleIO;
    public GameObject particleLoadPrefab;
    public GameObject content;

	private void OnEnable()
	{
		string[] names = particleIO.GetParticleNames();
		for(int i=0; i<names.Length; i++)
		{
			GameObject newChild = Instantiate(particleLoadPrefab, content.transform);
			newChild.GetComponentInChildren<ParticleLoadButton>().myText.text = System.IO.Path.GetFileName(names[i]);
		}
	}

	private void OnDisable()
	{
		int childs = content.transform.childCount;
		for(int i=0; i<childs; i++)
		{
			Destroy(content.transform.GetChild(i).gameObject);
		}
	}

	public void LoadParticle(string fileName)
	{
		particleIO.LoadConfirm(fileName);
		gameObject.SetActive(false);
	}
}
