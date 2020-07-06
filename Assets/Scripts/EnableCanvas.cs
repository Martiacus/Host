using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCanvas : MonoBehaviour
{
    public GameObject canvasToEnable;          // Here we set the canvas we are going to disable

    public void OnClick()
    {
        canvasToEnable.SetActive(true);       // Here we disable that canvas
    }
}
