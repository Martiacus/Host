using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CreateTablePlansSelectionButtons : MonoBehaviour
{
    public GameObject buttonPrefab;         // We instaciate this button for easier creating and modification purposes
    public GameObject data;                 // We collect all of the relevant data from this gameobject that stores it

    private string planName;                // The name of a single table plan we will use for creating the buttons for selecting plnas
    private int tableCount;                 // The total number of tables we have in that table plan
    private DateTime dateCreated;           // Date the current table was created

    private int plansCount;                 // Total number of table plans we have

    /// <summary>
    /// We need to run this on canvas enable so we can get the table plans list
    /// </summary>
    public void OnEnabling()
    {
        plansCount = data.GetComponent<TablePlans>().tablePlans.Count;
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 100 * plansCount);
        CreatePlans();
    }

    /// <summary>
    /// Creates table plans buttons
    /// </summary>
    public void CreatePlans()
    {
        GameObject temp;    // Temporary gameobject meant to manipulate our instanciated gameobject

        for (int i = 0; i < plansCount; i++)
        {
            SetDataToParams(i);                                                                             // Here we set data to our global parameters so we can use them
            temp = Instantiate(buttonPrefab, new Vector3(0,0,0), Quaternion.identity);                      // Here we create and set position of our new button
            temp.transform.parent = this.gameObject.transform;                                              // Here we set the parent of this button so it doesnt get lost
            temp.transform.localPosition = new Vector3(625, -350 - (i * 100), 0);                           // Here we set the position relative to the parent
            temp.transform.localScale = new Vector3(1, 1, 1);                                               // Here we reset the scale of our prefab so there are no hickups
            temp.transform.tag = "TablePlanSelectionButton";                                                // Here we set the tag for the gameobject for group manipulation
            temp.name = planName;                                                                           // Here we have the name of the gameobject
            temp.transform.GetChild(1).gameObject.GetComponent<Text>().text = planName;                                // Here we set and display the name of the table plan for identification purposes
            temp.transform.GetChild(2).gameObject.GetComponent<Text>().text = Convert.ToString(tableCount);            // Here we set and display total number of available tables for further identification purposes
            temp.transform.GetChild(3).gameObject.GetComponent<Text>().text = dateCreated.ToShortDateString();         // Here we set and display the date this table plan was created so we know if this is old table plan or newer 1
            SetButtonOnOrOFF(temp);                                                                                    // Here we set the button to an OFF state or ON state depending if it is already in the on button list
        }
        SaveController.SaveUserTablePlans(data.GetComponent<TablePlans>());
    }

    /// <summary>
    /// Here we set the button to on or of depending if it was already on during previous instances
    /// </summary>
    /// <param name="button"></param>
    private void SetButtonOnOrOFF(GameObject button)
    {
        button.transform.GetChild(0).gameObject.GetComponent<ButtonOnOff>().SetButtonToOff();

        for (int i = 0; i < data.GetComponent<TablePlans>().selectedTablePlans.Count; i++)
        {
           if(data.GetComponent<TablePlans>().ReturnNameAtIndex(i) == button.transform.GetChild(1).gameObject.GetComponent<Text>().text)
            {
                button.transform.GetChild(0).gameObject.GetComponent<ButtonOnOff>().SetButtonToOn();
            }
        }
    }

    /// <summary>
    /// Here we set data to parameters according to the table plan win index planID
    /// </summary>
    /// <param name="planID"> The id of the current table plan we are editing</param>
    private void SetDataToParams(int planID)
    {
        planName = data.GetComponent<TablePlans>().tablePlans[planID].GetPlanName();                        // Here we set the table plan name
        tableCount = data.GetComponent<TablePlans>().tablePlans[planID].GetTableCount();                    // Here we set the table count
        dateCreated = data.GetComponent<TablePlans>().tablePlans[planID].GetCreatedDate();                  // Here we set the date the plan was created
    }
}
