using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsTableImageFree : MonoBehaviour
{
    private bool isTableFree = true;

    /// <summary>
    /// Returns table availability
    /// </summary>
    /// <returns> If table is free returns true if nor false</returns>
    public bool IsTableFree()
    {
        return isTableFree;
    }

    /// <summary>
    /// Red is for table is taken
    /// </summary>
    public void SetTableColorToRed()
    {
        this.gameObject.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color32(200, 50, 50, 255);
        isTableFree = false;
    }

    /// <summary>
    /// Green is for table is free
    /// </summary>
    public void SetTableColorToGreen()
    {
        this.gameObject.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color32(50, 200, 50, 255);
        isTableFree = true;
    }

    /// <summary>
    /// Oragne is for table is taken but is booked for later
    /// </summary>
    public void SetTableColorToOrange()
    {
        this.gameObject.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color32(200, 100, 50, 255);
        isTableFree = false;
    }

    /// <summary>
    /// Blue is for table to be free but is booked for later date
    /// </summary>
    public void SetTableColorToBlue()
    {
        this.gameObject.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color32(50, 50, 200, 255);
        isTableFree = true;
    }

    /// <summary>
    /// Blue is for table to be free but is booked for later date
    /// </summary>
    public void SetTableColorToYellow()
    {
        this.gameObject.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color32(255, 255, 100, 255);
        isTableFree = true;
    }
}