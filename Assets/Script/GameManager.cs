using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CameraControl cameraControl;
    public CharacterControl characterControl;
    public CharacterInfo playerInfo;
    public BoltEntity playerEntity;

    public SkillInfo skill1;
    public SkillInfo skill2;
    public SkillInfo skill3;
    public SkillInfo skill4;

    public Particle[,] particle;

    public string file = "C:/Users/santa/Desktop/Particles/New Particle";

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
		}
		else
		{
            Destroy(gameObject);
		}
    }
}
