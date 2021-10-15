using UnityEngine;
using System.Collections.Generic;

public class InputControl : MonoBehaviour
{
	public CameraControl cameraControl;
	public CharacterControl characterControl;

	private Coroutine usingSkill;

	public KeyCode[] skill = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R };

	[HideInInspector]
	public enum SKILLMODE
	{
		INSTANT, // ���
		MOUSECLICK, // Ŭ������ ���
		MOUSEUP // ���콺 Ŭ�� ������ ���
	}

	public Dictionary<KeyCode, SKILLMODE> skillMode = new Dictionary<KeyCode, SKILLMODE>();
	private List<KeyCode> skillRegistered;

	private KeyCode skillSet;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Confined;
		for(int i=0; i<4; i++)
		{
			skillMode.Add(skill[i], SKILLMODE.INSTANT);
		}
		skillRegistered = new List<KeyCode>(skillMode.Keys);
	}

	// Update is called once per frame
	void Update()
	{
		// ��Ŭ��(�̵� + ��Ÿ)
		if (Input.GetMouseButtonDown(1))
		{
			characterControl.RightClickInput(Input.mousePosition);
		}

		// ��ų���
		for (int i = 0; i < skillRegistered.Count; i++) {
			if (Input.GetKeyDown(skillRegistered[i])){
				switch (skillMode[skillRegistered[i]])
				{
					case SKILLMODE.INSTANT:
						usingSkill = StartCoroutine(characterControl.SkillShoot(Input.mousePosition, i)); // i��° ��ų ���
						break;
				}
			}
		}

		//ī�޶� �̵�
		if (Input.GetKey(KeyCode.Space))
		{
			cameraControl.Reset();
		}

		if (Input.mouseScrollDelta.y != 0)
		{
			cameraControl.ChangeSight(Input.mouseScrollDelta.y);
		}

		cameraControl.MoveScreen(Input.mousePosition);
	}
}
