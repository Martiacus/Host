using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Time : MonoBehaviour
{
    /// <summary>
    /// Displays current time
    /// </summary>
    private void Update()
    {
        this.gameObject.GetComponent<Text>().text = DateTime.Now.ToString();
    }
}