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
		INSTANT, // 즉발
		MOUSECLICK, // 클릭으로 사용
		MOUSEUP // 마우스 클릭 해제시 사용
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
		// 우클릭(이동 + 평타)
		if (Input.GetMouseButtonDown(1))
		{
			characterControl.RightClickInput(Input.mousePosition);
		}

		// 스킬사용
		for (int i = 0; i < skillRegistered.Count; i++) {
			if (Input.GetKeyDown(skillRegistered[i])){
				switch (skillMode[skillRegistered[i]])
				{
					case SKILLMODE.INSTANT:
						usingSkill = StartCoroutine(characterControl.SkillShoot(Input.mousePosition, i)); // i번째 스킬 사용
						break;
				}
			}
		}

		//카메라 이동
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
