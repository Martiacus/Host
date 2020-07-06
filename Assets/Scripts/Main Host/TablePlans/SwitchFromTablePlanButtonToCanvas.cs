using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFromTablePlanButtonToCanvas : MonoBehaviour
{
    /// <summary>
    /// Here we enable the enalrged plan cavas disabling disruptive main cavas elements
    /// </summary>
    public void OnClick()
    {
        Controller controller = GameObject.Find("Controller").GetComponent<Controller>();           // Our main controller that stores all of our active plans and tables
        controller.SetPlanToActive(this.gameObject.transform.parent.name);                          // Here we set plan to active with the name of the parent gameobject
        controller.exitButtonForMainCanvas.SetActive(false);                                        // Here we set the exit button to inactive bauces it interferes with our close button for plan canvas
        this.gameObject.transform.parent.parent.gameObject.SetActive(false);                        // Here we disable this gameobjects parents parent wich is the main gameobject for main canvas
    }
}
