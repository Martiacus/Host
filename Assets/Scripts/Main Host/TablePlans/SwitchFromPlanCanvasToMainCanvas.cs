using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFromPlanCanvasToMainCanvas : MonoBehaviour
{
    /// <summary>
    /// Here we enable and disable required components for switching between canvases
    /// </summary>
    public void OnClick()
    {
        Controller controller = GameObject.Find("Controller").GetComponent<Controller>();       // Our controller where all current plan and table data is stored
        controller.exitButtonForMainCanvas.SetActive(true);                                     // Here we set the exit button to active
        controller.SetMainCanvasToActive(this.gameObject.transform.parent.name);                // Here we set the parent of that plan button to active
        this.gameObject.transform.parent.gameObject.SetActive(false);                           // Here we disable this enlarged plan canvas
    }
}
