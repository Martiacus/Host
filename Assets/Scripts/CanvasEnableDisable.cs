using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to walk through different canvases in main menu
/// </summary>
public class CanvasEnableDisable : MonoBehaviour
{
    public GameObject CanvasToEnable;   // We enable this canvas after button is pressed
    public GameObject CanvasToDisable;  // We disable this canvas (most likelly the canvas we are using now) after buttoon is pressed

        public void EnableCanvas()
    {
        CanvasToEnable.SetActive(true);     // Enabling a canvas
        CanvasToDisable.SetActive(false);   // Disabling a canvas
    }
}
