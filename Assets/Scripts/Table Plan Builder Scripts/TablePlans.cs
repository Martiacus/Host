using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class TablePlans : MonoBehaviour
{
    public List<TablePlan> tablePlans;  // Here we store all of our table plans

    public Text nameField;              // Here we get the table plan name
    public GameObject buttonInfo;       // Here we get all the buttons with their coordinates and color ids
    public GameObject gridInfo;         // Here we get how many items we have in x and y axis

    public List<string> selectedTablePlans;         // Here we set the selected table plans by name to be used in main host menu

    public List<TablePlan> onTablePlans;            // Here we store a copy of all of our on table plans

    /// <summary>
    /// Here we assign all the data to their respective fields
    /// </summary>
    public void GetData()
    {
        tablePlans.Add(
            new TablePlan(
                nameField.GetComponent<Text>().text,
                gridInfo.GetComponent<CreateTablePlanButtonGrid>().GetTablePlanBuilderWidth(),
                gridInfo.GetComponent<CreateTablePlanButtonGrid>().GetTablePlanBuilderHeight(),
                buttonInfo.GetComponent<TablePlanBuilderCollectButtonsInfo>().collectedButtons,
                DateTime.Now
            ));
    }

    /// <summary>
    /// Returns all selected table plans
    /// </summary>
    /// <returns> All selectd table plans</returns>
    public List<string> ReturnSelectedTablePlans()
    {
        return selectedTablePlans;
    }

    /// <summary>
    /// Here we find all of our on table plans
    /// </summary>
    /// <returns></returns>
    public void FindOnTablePlans()
    {
        onTablePlans.Clear();

        for (int i = 0; i < tablePlans.Count; i++)
        {
            for (int j = 0; j < selectedTablePlans.Count; j++)
            {
                if (tablePlans[i].GetPlanName() == selectedTablePlans[j])
                {
                    onTablePlans.Add(tablePlans[i]);
                }
            }
        }
    }

    /// <summary>
    /// Here we cycle through all the table plans and remove the 1st table plan with the matching name
    /// </summary>
    /// <param name="planName"> The name we are looking for to delete</param>
    public void DeleteTablePlan(string planName)
    {
        for (int i = 0; i < tablePlans.Count; i++)
        {
            if (tablePlans[i].GetPlanName() == planName)
            {
                tablePlans.RemoveAt(i);
                break;
            }
        }
    }

    /// <summary>
    /// This can only return a single plan because all names are unique
    /// </summary>
    /// <param name="name"> The name we are comparing to</param>
    /// <returns> Table plan with that name</returns>
    public TablePlan ReturnPlanByName(string name)
    {
        foreach(TablePlan tableplan in tablePlans)
        {
            if(tableplan.GetPlanName() == name)
            {
                return tableplan;
            }
        }
        return null;
    }

    /// <summary>
    /// Here we return a plan name at index
    /// </summary>
    /// <param name="index"> The index we are looking at</param>
    /// <returns> Name of the table plan</returns>
    public string ReturnNameAtIndex(int index)
    {
        return selectedTablePlans[index];
    }

    /// <summary>
    /// Here we find only selected table plans
    /// </summary>
    /// <param name="plans"></param>
    public void SetSelectedPlans(List<string> plans)
    {
        selectedTablePlans = new List<string>();        // Here we use a new list because we dont want to add any new plans
        selectedTablePlans = plans;
    }

    /// <summary>
    /// Returns a whole list of all table plans
    /// </summary>
    /// <returns> returns</returns>
    public List<TablePlan> ReturnTablePlans()
    {
        return tablePlans;
    }

    /// <summary>
    /// Returns a whole list of on table plans
    /// </summary>
    /// <returns> On table plans</returns>
    public List<TablePlan> ReturnOnTablePlans()
    {
        return onTablePlans;
    }

    /// <summary>
    /// Here we set loaded data from save file
    /// </summary>
    /// <param name="savedTablePlans"> Table plans</param>
    /// <param name="savedSelectedTablePlans"> Selected table plans</param>
    /// <param name="savedOnTablePlans"> On table plaans</param>
    public void SetLoadedData(List<TablePlan> savedTablePlans, List<string> savedSelectedTablePlans, List<TablePlan> savedOnTablePlans)
    {
        tablePlans = savedTablePlans;
        selectedTablePlans = savedSelectedTablePlans;
        onTablePlans = savedOnTablePlans;
    }
}
