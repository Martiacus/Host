using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOnOff : MonoBehaviour
{
    public bool isButtonOn;             // Tells us if button is on or off. we can use this to collect plans info wich are on wich are off

    /// <summary>
    /// Sets the button color and text to off
    /// </summary>
    public void SetButtonToOff()
    {
        this.gameObject.GetComponent<Image>().color = new Color(255, 0, 0);
        this.gameObject.GetComponentInChildren<Text>().text = "OFF";
        isButtonOn = false;
    }

    /// <summary>
    /// Sets the button color and text to on
    /// </summary>
    public void SetButtonToOn()
    {
        this.gameObject.GetComponent<Image>().color = new Color(0, 255, 0);
        this.gameObject.GetComponentInChildren<Text>().text = "ON";
        isButtonOn = true;

    }

    /// <summary>
    /// Changes between table on and off states
    /// </summary>
    public void OnClick()
    {
        if (isButtonOn)
        {
            SetButtonToOff();
        }
        else
        {
            SetButtonToOn();
        }
    }
}
