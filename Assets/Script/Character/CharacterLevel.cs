using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLevel : MonoBehaviour
{
    [System.NonSerialized]
    public CharacterInfo characterInfo;

	private void Update()
	{
		if(characterInfo.exp.get() >= characterInfo.expMax.get())
		{
			characterInfo.exp.add(-characterInfo.expMax.get());
			characterInfo.LevelUp();
		}
	}
}
