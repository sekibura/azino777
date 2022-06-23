using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidInput : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ViewManager.ShowLast();
            }
        }
    }
}
