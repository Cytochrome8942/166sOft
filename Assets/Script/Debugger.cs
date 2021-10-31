using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public void Faster()
	{
		Time.timeScale = 3f;
	}

	public void Normal()
	{
		Time.timeScale = 1f;
	}
}
