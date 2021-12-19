using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class CharacterLevel : EntityBehaviour<IMinionState>
{
    [System.NonSerialized]
    public CharacterInfo characterInfo;

	private void Update()
	{
		if(characterInfo.exp.get() >= characterInfo.expMax.get())
		{
			characterInfo.exp.add(-characterInfo.expMax.get());
			characterInfo.LevelUp();
			state.Level = characterInfo.level.get();
		}
	}
}
