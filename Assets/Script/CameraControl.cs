using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	public float moveSpeed;

	public float maxX;
	public float maxZ;

	private float sight = 0f;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Confined;
	}

	public void MoveScreen(Vector3 mousePosition)
	{
		if (mousePosition.x < Screen.width * 0.02f && transform.position.x > -maxX)
		{
			transform.position += new Vector3(-moveSpeed, 0, 0);
		}
		if (mousePosition.x > Screen.width * 0.98f && transform.position.x < maxX)
		{
			transform.position += new Vector3(moveSpeed, 0, 0);
		}
		if (mousePosition.y < Screen.height * 0.02f && transform.position.z > -maxZ)
		{
			transform.position += new Vector3(0, 0, -moveSpeed);
		}
		if (mousePosition.y > Screen.height * 0.98f && transform.position.z < maxZ)
		{
			transform.position += new Vector3(0, 0, moveSpeed);
		}
	}

	public void ChangeSight(float scrollData)
	{
		if((scrollData > 0 && sight < 10) || (scrollData < 0 && sight > -10))
		{
			sight += scrollData;
			transform.position += transform.forward * scrollData;
		}
	}

	public void Reset()
	{
		transform.position = GameManager.instance.characterControl.transform.position + new Vector3(0, 25.3f, -7.6f) + transform.forward * sight;
	}
}
