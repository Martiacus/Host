using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TablePlanBuilderCollectButtonsInfo : MonoBehaviour
{
    //Here we store a single button info for future use

    //Here we make a list of collected buttons
    public List<CollectedButtonsClass> collectedButtons;

    /// <summary>
    /// Here we find all buttons with their tag and set them to CollectedButtons class on the list
    /// </summary>
    public void OnClick()
    {
        collectedButtons = new List<CollectedButtonsClass>();
        GameObject[] buttons;

        buttons = GameObject.FindGameObjectsWithTag("TablePlanBuilderButton");
        foreach (GameObject button in buttons)
        {
            collectedButtons.Add(new CollectedButtonsClass(button.GetComponent<TablePlanBuilderButtonInfo>().GetXCoordinate(), button.GetComponent<TablePlanBuilderButtonInfo>().GetYCoordiante(), button.GetComponent<TablePlanBuilderButtonInfo>().GetColorID()));
        }
    }
}
