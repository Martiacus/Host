using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllTablePlanButtons : MonoBehaviour
{
    /// <summary>
    /// Destroys all table plan buttons on exiting the canvas and all the table plan canvases
    /// </summary>
    public void OnClick()
    {
        GameObject[] buttons;

        buttons = GameObject.FindGameObjectsWithTag("TablePlanHostButton");
        foreach (GameObject button in buttons)
        {
            Destroy(button.gameObject);
        }

        buttons = GameObject.Find("Controller").GetComponent<Controller>().ReturnAllPlans();
        foreach (GameObject button in buttons)
        {
            Destroy(button.gameObject);
        }
    }
}
