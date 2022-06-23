using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;
    public event Action Playing;
    public event Action Stoped;
 

    private void Awake()
    {
        Instance = this;
    }

    public void StartPlaying()
    {
        Playing?.Invoke();
    }

    public void StopPlaying()
    {
        Stoped?.Invoke();
    }
 
}
