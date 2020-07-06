using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectOnTablePlans : MonoBehaviour
{
    private List<string> namesOfOnTablePlans;

    public GameObject data;

    /// <summary>
    /// Here we collect all of the ON table plans and put them in a list
    /// </summary>
    public void OnClick()
    {
        namesOfOnTablePlans = new List<string>();

        GameObject[] buttons;

        buttons = GameObject.FindGameObjectsWithTag("TablePlanSelectionButton");
        foreach (GameObject button in buttons)
        {
            if(button.transform.GetChild(0).GetComponent<ButtonOnOff>().isButtonOn)
            {
                namesOfOnTablePlans.Add(button.transform.GetChild(1).gameObject.GetComponent<Text>().text);
            }
        }
        data.GetComponent<TablePlans>().SetSelectedPlans(namesOfOnTablePlans);
        SaveController.SaveUserTablePlans(data.GetComponent<TablePlans>());
    }
}
