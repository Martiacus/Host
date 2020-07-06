using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableWantsToMoveController : MonoBehaviour
{
    public MainHostController mainHostController;       // This is our main controller that manages all the data for hosting
    private GameObject tableThatWantsToMove;            // This is the table that wants to move
    private List<TablePlans> allTablePlans;             // Theese are all the selected table plans
    public Controller controller;                       // This is our main controller where we get the selected table plans

    private class TablePlans
    {
        string name;            // Table plan name
        GameObject plan;        // Table plan gameobject to check current table availability
        bool isSelected;        // Here we determine if the current table plan is selected

        public TablePlans(string senderName, GameObject senderGameobject)
        {
            name = senderName;
            plan = senderGameobject;
            isSelected = false;
        }

        /// <summary>
        /// Returns the original plan gameobjecet
        /// </summary>
        /// <returns> Plan gameobject</returns>
        public GameObject GetPlanGameobject()
        {
            return plan;
        }

        /// <summary>
        /// Here we return table plan selection status. True if seleted false if not
        /// </summary>
        /// <returns></returns>
        public bool IsTablePlanSelected()
        {
            return isSelected;
        }

        /// <summary>
        /// Here we return the table plan name
        /// </summary>
        /// <returns>Table plan name</returns>
        public string GetPlanName()
        {
            return name;
        }

        /// <summary>
        /// Here we set the table plan as selected
        /// </summary>
        public void SetAsActive()
        {
            isSelected = true;
        }

        /// <summary>
        /// Here we set the table plan as not selected
        /// </summary>
        public void SetAsInactive()
        {
            isSelected = false;
        }
    }

    /// <summary>
    /// Here we receive the table that wants to move
    /// </summary>
    /// <param name="senderGameObject"> Table gameobject</param>
    public void ReceiveTable(GameObject senderGameObject)
    {
        tableThatWantsToMove = senderGameObject;
    }

    /// <summary>
    /// Returns the full list of plan gamobjects
    /// </summary>
    /// <returns> All original table plan gameobjects</returns>
    public List<GameObject> GetTablePlans()
    {
        allTablePlans = new List<TablePlans>();
        controller.GetSeletedTablePlans();

        List<GameObject> plans = new List<GameObject>();

        foreach (TablePlans plan in allTablePlans)
        {
            plans.Add(plan.GetPlanGameobject());
        }

        return plans;
    }

    /// <summary>
    /// Here we receive the selected table plans data one by one
    /// </summary>
    /// <param name="senderName"> The table plan name</param>
    /// <param name="senderGameObject"> The table plan gameobject</param>
    public void ReceiveData(string senderName, GameObject senderGameObject)
    {
        TablePlans tablePlan = new TablePlans(senderName, senderGameObject);
        allTablePlans.Add(tablePlan);
    }

    /// <summary>
    /// Here we find a table plan with a specific name and set it as selected(all table plan names are unique)
    /// </summary>
    /// <param name="name"> Table plan name</param>
    public void SelectATablePlan(string name)
    {
        foreach(TablePlans tempPlan in allTablePlans)
        {
            if(tempPlan.GetPlanName() == name)
            {
                tempPlan.SetAsActive();
                return;
            }
        }
    }

    /// <summary>
    /// Here we find a table plan with a specific name and set it as selected(all table plan names are unique)
    /// </summary>
    /// <param name="name"> Table plan name</param>
    public void DeselectATablePlan(string name)
    {
        foreach (TablePlans tempPlan in allTablePlans)
        {
            if (tempPlan.GetPlanName() == name)
            {
                tempPlan.SetAsInactive();
                return;
            }
        }
    }

    /// <summary>
    /// Here we save the selection
    /// </summary>
    public void SaveTablePlanSelection()
    {
        if (ReturnSelectedTablePlans().Count == 0)
        {
            tableThatWantsToMove.GetComponent<TableButtonInfo>().CancelWantsToMove();
        }
        else
        {
            tableThatWantsToMove.GetComponent<TableButtonInfo>().ReceiveListOfTablePlansToMoveTo(ReturnSelectedTablePlans());
        }
    }

    /// <summary>
    /// Here we find all the table plans list that is selected for moving
    /// </summary>
    private List<GameObject> ReturnSelectedTablePlans()
    {
        List<GameObject> plans = new List<GameObject>();
        if(GetSelectedTableCount() != 0)
        {
            foreach(TablePlans plan in allTablePlans)
            {
                if(plan.IsTablePlanSelected())
                {
                    plans.Add(plan.GetPlanGameobject());
                }
            }
        }

        return plans;
    }

    /// <summary>
    /// Returns selected table plan count
    /// </summary>
    /// <returns> Selected table plan count</returns>
    public int GetSelectedTableCount()
    {
        int count = 0;
        foreach (TablePlans plan in allTablePlans)
        {
            if(plan.IsTablePlanSelected())
            {
                count++;
            }
        }
        return count;
    }
}
