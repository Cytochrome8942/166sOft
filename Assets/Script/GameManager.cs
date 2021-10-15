using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CameraControl cameraControl;
    public CharacterControl characterControl;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
}
