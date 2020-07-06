using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCanvas : MonoBehaviour
{
    public GameObject canvasToDisable;          // Here we set the canvas we are going to disable

    public void OnClick()
    {
        canvasToDisable.SetActive(false);       // Here we disable that canvas
    }
}
