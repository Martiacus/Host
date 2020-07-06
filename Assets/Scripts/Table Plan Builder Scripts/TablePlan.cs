using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class TablePlan
{
    string planName;        // Table Plan name
    int xAxisCount;         // Table plan X axis objects count
    int yAxisCount;         // Table plan Y axis objects count
    DateTime dateCreated;       // Date created

    List<CollectedButtonsClass> collectedButtons = new List<CollectedButtonsClass>();   // Here we get collected buttons with their coordinates and color

    /// <summary>
    /// Here we assign all the attributes from
    /// </summary>
    /// <param name="name1"> Name</param>
    /// <param name="xCount"> X axis objects count</param>
    /// <param name="yCount"> Y axis obhects count</param>
    /// <param name="buttons"> A list of buttons with their coordinates and color id</param>
    public TablePlan(string name1, int xCount, int yCount, List<CollectedButtonsClass> buttons, DateTime date)
    {
        planName = name1;
        xAxisCount = xCount;
        yAxisCount = yCount;
        collectedButtons = buttons;
        dateCreated = date;
    }

    /// <summary>
    /// Here we return all of our collected buttons of a single plan
    /// </summary>
    /// <returns> All the collected buttons of a single plan</returns>
    public List<CollectedButtonsClass> GetCollectedButtons()
    {
        return collectedButtons;
    }

    /// <summary>
    /// Here we get the time the table plan was created
    /// </summary>
    /// <returns> Date the plan was created</returns>
    public DateTime GetCreatedDate()
    {
        return dateCreated;
    }

    /// <summary>
    /// Here we return the name of this table plan
    /// </summary>
    /// <returns> Plan name</returns>
    public string GetPlanName()
    {
        return planName;
    }
    
    /// <summary>
    /// Here we return the ammount of rows we have in this table plan
    /// </summary>
    /// <returns> Row count</returns>
    public int GetXAxisCount()
    {
        return xAxisCount;
    }

    /// <summary>
    /// Here we return the ammount of columns we have in this table plan
    /// </summary>
    /// <returns> Column count</returns>
    public int GetYAxisCount()
    {
        return yAxisCount;
    }

    /// <summary>
    /// Calculates and returns table count
    /// </summary>
    /// <returns></returns>
    public int GetTableCount()
    {
        int temp = 0;
        foreach (CollectedButtonsClass list in collectedButtons)
        {
            if (list.GetColorID() == 2)
            {
                temp++;
            }
        }
        return temp;
    }
}
