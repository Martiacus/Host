using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTablePlansCanvasesByName : MonoBehaviour
{
    public GameObject data;                     // Our Global gameobject that stores all table plans and tables
    public GameObject tablePlanCanvasPrefab;    // Here we have our table plan canvas prefab for easier instanciation

    /// <summary>
    /// This has to go after CreateTablePlanButtons in main menu buttonStart onclick hierarchy
    /// </summary>
    public void OnClick()
    {
        GameObject tempGameobject;

        foreach (TablePlan plan in data.GetComponent<TablePlans>().onTablePlans)
        {
            tempGameobject = Instantiate(tablePlanCanvasPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            tempGameobject.transform.SetParent(this.gameObject.transform);                              // Here we assign a parent for easier use
            tempGameobject.transform.localScale = new Vector3(1, 1, 1);                                 // Here we set the scale to 1 so there are no misinstructions
            tempGameobject.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);                 // Here we set the gameobject transform to recctransform
            tempGameobject.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);                 // Here we set left and bottom rect transform
            tempGameobject.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);                 // Here we set right and top rect transform
            tempGameobject.name = plan.GetPlanName();                                                   // Here we set the canvas plan name
            tempGameobject.transform.GetChild(2).GetComponent<Text>().text = plan.GetPlanName();        // Here we set the text parameter of the child component to display the correct plan name
            tempGameobject.tag = "TablePlanCanvas";                                                     // Here we set the table plan tag
            GameObject.Find("Controller").GetComponent<Controller>().AddGameobjectToPlan(tempGameobject.name, tempGameobject);          // Here we find our main controller and assign this plan gamobject to the controller to be accessed any time
            tempGameobject.gameObject.transform.GetChild(3).GetComponent<CreateTablePlanForCanvas>().CreateTablesAndWalls(plan);        // Here we create the buttons and walls for this table plan
            tempGameobject.SetActive(false);                                                            // Here we set the table plan canvas to inactive because we only need it to show up after we click the button to enable it
        }
    }
}