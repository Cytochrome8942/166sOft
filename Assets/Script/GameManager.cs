using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CameraControl cameraControl;
    public CharacterControl characterControl;

    public CharacterInfo playerInfo;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
}
