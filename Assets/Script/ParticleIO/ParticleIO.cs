using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System.Windows.Forms;
using Ookii.Dialogs;

public class ParticleIO : MonoBehaviour
{
	[SerializeField]
	private Particle currentParticle;
	public ParticleBuilder particleBuilder;

	//Main Popup
	public ParticleBuilder startPopup;
	public ParticleBuilder middlePopup;
	public ParticleBuilder endPopup;

	//Main Canvas
	public Canvas startCanvas;
	public Canvas middleCanvas;
	public Canvas endCanvas;

	//Extra Popup
	public GameObject loadPopup;
	public GameObject newParticlePopup;
	public GameObject changeNamePopup;
	public GameObject particleTypePopup;

	//Image input
	VistaOpenFileDialog openFileDialog;
	Stream openStream = null;


	private string newName;

	public TMP_InputField nameText;

	private void Start()
	{
		openFileDialog = new VistaOpenFileDialog
		{
			Title = "이미지 파일"
		};
	}

	public void SaveParticle()
	{
		string filePath = "C:/Users/santa/Desktop" + "/Particles/" + currentParticle.particleName;
//		string filePath = Application.dataPath + "/Particles/" + currentParticle.particleName;

		string jsonString = JsonUtility.ToJson(currentParticle);
		File.WriteAllText(filePath, jsonString);

		Debug.Log("saved " + currentParticle.particleName);
	}

	public void LoadParticle()
	{
		loadPopup.SetActive(true);
	}

	public string[] GetParticleNames()
	{
		string filePath = "C:/Users/santa/Desktop" + "/Particles";
//		string filePath = Application.dataPath + "/Particles"
		return Directory.GetFiles(filePath);
	}

	public void LoadConfirm(string particleName)
	{
		//파일 위치의 파일명을 열어줌. 
		string fileName = "C:/Users/santa/Desktop" + "/Particles/" + particleName;
//		string fileName = Application.dataPath + "/Particles/" + particleName;
		
		if (File.Exists(fileName))
		{
			string jsonString = File.ReadAllText(fileName);
			currentParticle = new Particle();
			JsonUtility.FromJsonOverwrite(jsonString, currentParticle);
			currentParticle.SaveProperties();
			CallInitialize();
			particleBuilder.loader.Invoke();
		}
		else
		{
			Debug.Log("No such file error");
		}
	}

	public void NewParticle()
	{
		if (currentParticle != null)
		{
			newParticlePopup.SetActive(true);
		}
		else
		{
			NewParticleConfirm(false);
		}
	}

	public void NewParticleType(string type)
	{
		currentParticle.particleType = type;
		CallInitialize();
		particleBuilder.initializer.Invoke();
		particleTypePopup.SetActive(false);
	}

	public void NewParticleConfirm(bool isSaving)
	{
		if (isSaving)
		{
			SaveParticle();
		}
		newParticlePopup.SetActive(false);
		particleTypePopup.SetActive(true);

		currentParticle = new Particle();
		currentParticle.SaveProperties();
	}


	public void ChangeParticleName(string newName)
	{
		string checkFileName = "C:/Users/santa/Desktop" + "/Particles/" + newName;
		//		string checkFileName = Application.dataPath + "/Particles/" + newName;

		this.newName = newName;

		// 바꾸려는 이름의 파일이 이미 존재하는지 확인
		if (File.Exists(checkFileName))
		{
			changeNamePopup.SetActive(true);
		}
		else
		{
			// 현재 파일이 이미 저장되어 있는 파일인지 확인
			checkFileName = "C:/Users/santa/Desktop" + "/Particles/" + currentParticle.particleName;
			//checkFileName = Application.dataPath + "/Particles/" + currentParticle.particleName;
			if (File.Exists(checkFileName))
			{
				ConfirmChangeName();
			}
			else
			{
				currentParticle.particleName = newName;
			}
		}
	}

	public void ConfirmChangeName()
	{
		string fileName = "C:/Users/santa/Desktop" + "/Particles/" + currentParticle.particleName;
//		string fileName = Application.dataPath + "/Particles/" + currentParticle.particleName;

		if (File.Exists(fileName))
		{
			File.Delete(fileName);
		}

		currentParticle.particleName = newName;
		SaveParticle();
		changeNamePopup.SetActive(false);
	}

	public void CancelChangeName()
	{
		nameText.text = currentParticle.particleName;
		changeNamePopup.SetActive(false);
	}

	private void CallInitialize()
	{
		startCanvas.enabled = false;
		middleCanvas.enabled = false;
		endCanvas.enabled = false;
		switch (currentParticle.particleType)
		{
			case "S":
				particleBuilder = startPopup;
				startCanvas.enabled = true;
				break;
			case "M1":
				particleBuilder = middlePopup;
				middleCanvas.enabled = true;
				break;
			case "E":
				particleBuilder = endPopup;
				endCanvas.enabled = true;
				break;
		}
		particleBuilder.currentParticle = currentParticle;
		particleBuilder.particleNameHolder.text = currentParticle.particleName;
	}

	private string FileOpen()
	{
		if(openFileDialog.ShowDialog() == DialogResult.OK)
		{
			if((openStream = openFileDialog.OpenFile()) != null)
			{
				return openFileDialog.FileName;
			}
		}
		return null;
	}

	public Texture2D LoadTexture()
	{
		string filePath = FileOpen();
		byte[] bytes = File.ReadAllBytes(filePath);
		Texture2D loadedTexture = new Texture2D(2, 2);
		loadedTexture.LoadImage(bytes);
		return loadedTexture;
	}
}
