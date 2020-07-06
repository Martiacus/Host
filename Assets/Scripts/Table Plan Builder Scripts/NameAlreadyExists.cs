using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameAlreadyExists : MonoBehaviour
{
    public GameObject data;                         // Here we have all of our table plans

    /// <summary>
    /// Here we check if the name already exists (we can only have unique names)
    /// </summary>
    /// <returns> True if the name exists and false if it doesnt</returns>
    public bool CheckIfNameExsits()
    {
        // here we get and check if the table plan already exists or not with a name attached to this gameobject
        for (int i = 0; i < data.GetComponent<TablePlans>().tablePlans.Count; i++)      // Here we get total number of plans
        {
            if (data.GetComponent<TablePlans>().tablePlans[i].GetPlanName() == GetComponent<InputField>().text)         // Here we check if the plan on this gameobject matches any of our saved ones
            {
                GetComponent<InputField>().text = null;                     // If any name matches we set the name to null to not repeat the mistake
                return true;
            }
        }
        return false;               // We only return false after checking all the names
    }

    /// <summary>
    /// Here we check if the name is valid with our manually added invalid names list
    /// </summary>
    /// <returns></returns>
    public bool CheckIfNameIsValid()
    {
        List<string> names = InvalidNames();                // Here we assing all the invalid names to a string list

        foreach (string name in names)
        {
            if (name == GetComponent<InputField>().text)        // Here we check if the plan name matches the invalid names
            {
                return false;                                   // Here we return false because the name matches invalid names and and there for is not valid
            }
        }
        return true;                                            // Here we return true because none of the invalid names match the plan name
    }

    /// <summary>
    /// Here we manually add the invalid names for table plans or the program will crash dua to Gameobject.Find function not finding the correct gameobjects
    /// </summary>
    /// <returns> A list of invalid names</returns>
    private List<string> InvalidNames()
    {
        List<string> names = new List<string>();
        names.Add("Controller");
        names.Add("TablePlans");
        names.Add("");
        names.Add("MainHostController");
        names.Add("WantsToMoveCanvas");

        return names;
    }
}
