using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
#if UNITY_ANDROID
        Screen.fullScreen = false;
#endif

    }
}
