using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetResolution : MonoBehaviour
{
    public int width;       // Resolution width
    public int height;      // Resolution height

    /// <summary>
    /// Here we set the screen resolution to match our app
    /// </summary>
    void Start()
    {
        Screen.SetResolution(width, height, true);
    }
}