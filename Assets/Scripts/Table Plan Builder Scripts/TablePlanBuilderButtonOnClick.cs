using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TablePlanBuilderButtonOnClick : MonoBehaviour
{
    public int colorID;

    /// <summary>
    /// Here we change the color on click depending on color id
    /// </summary>
    public void OnClick()
    {
        colorID += 1;

        // Here we change the button color in sequence depending on the colorID
        if(colorID > 2)
        {
            colorID = 0;
            SetColorToWhite();
        }
        else if(colorID == 1)
        {
            SetColorToBlack();
        }
        else if (colorID == 2)
        {
            SetColorToGreen();
        }
        this.GetComponent<TablePlanBuilderButtonInfo>().SetColorID(colorID);    // We send the current color id to button info
    }

    /// <summary>
    /// Here we set the color to black
    /// </summary>
    private void SetColorToBlack()
    {
        colorID = 1;
        this.gameObject.GetComponent<Image>().color = new Color(50, 50, 50, 100);
    }

    /// <summary>
    /// Here we set the color to white
    /// </summary>
    private void SetColorToWhite()
    {
        colorID = 0;
        this.gameObject.GetComponent<Image>().color = new Color(200, 200, 200, 100);
    }

    // Here we set the color to green
    private void SetColorToGreen()
    {
        colorID = 2;
        this.gameObject.GetComponent<Image>().color = new Color(50,200,50,100);
    }
}