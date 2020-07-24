using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TableButtonInfo : MonoBehaviour
{
    private string tablePlanName;       // Name of the table plan this table belongs to
    private bool isTableTaken;          // Here we set if the table is taken or not
    private int pax;                    // How many people are seated on this table
    private DateTime projectedLeavingTime;   // The time the table is guessed to leave at
    private DateTime timeSeated;        // The time the table was seated
    private bool tableWantsToMove;      // Here we set the bool to true that the table wants to move false if not
    private bool isTableAbleToMove;     // Here we specify if there are any free tables that this table can move to from table plans list
    private bool isTableMarked;         // Table marked status

    public GameObject mainController;   // Canvas we are contacting for creating a new table
    public GameObject controller;       // This is our main system controller

    private List<GameObject> tablePlansThatTableWantsToMoveTo;  // This is a list of table plans that the table wants to move to

    private List<String> tablePlansThatTableCanMoveTo;      // A list if table plan names that the table can move to


    /// <summary>
    /// Increases the projected table leaving time
    /// </summary>
    /// <param name="increase"></param>
    public void IncreaseTableStayTime(int increase)
    {
        projectedLeavingTime = projectedLeavingTime.AddMinutes(increase);
    }

    /// <summary>
    /// Here we reset the data for wants to move to regular table
    /// </summary>
    public void CancelWantsToMove()
    {
        SetTableColorToRed();
        tableWantsToMove = false;
        mainController.GetComponent<MainHostController>().RemoveFromTablesThatWantToMoveList(this.gameObject);
        mainController.GetComponent<MainHostController>().GetDisplayTableInfoCanvas().GetComponent<TableInfoCanvasController>().UpdateMovingList(null);
        tablePlansThatTableCanMoveTo = null;
    }

    /// <summary>
    /// Sends a message to notifications canvas to display
    /// </summary>
    /// <param name="message"></param>
    private void SendNotification(string message)
    {
        mainController.GetComponent<MainHostController>().AddNotification(message);
    }

    /// <summary>
    /// Sets the name of the parent this gameobject belongs to
    /// </summary>
    public void SetTablePlanName(string planName)
    {
        tablePlanName = planName;
    }

    /// <summary>
    /// A list of table plans that the table can move to to display for the user
    /// </summary>
    /// <param name="plans"></param>
    public void SetTablePlansListThatTableIsAbleToMoveTo(List<string> plans)
    {
        tablePlansThatTableCanMoveTo = plans;
    }

    /// <summary>
    /// Sends an update to information canvas about availability of tables in plans
    /// </summary>
    private void UpdateFreePlansDataInInfoCanvas()
    {
        if (mainController.GetComponent<MainHostController>().GetInfoCanvas().activeSelf)
        {
            mainController.GetComponent<MainHostController>().GetInfoCanvas().GetComponent<TableInfoCanvasController>().UpdateMovingList(tablePlansThatTableCanMoveTo);
        }
    }

    /// <summary>
    /// Sets table color so that the user knows the table wants to move but cannot yet
    /// </summary>
    public void SetTableAsUnableToMove()
    {
        SetTableColorToOrange();
        isTableAbleToMove = false;
    }

    /// <summary>
    /// Sets the table colo so that the user knows this table is ready to move
    /// </summary>
    public void SetTableAsAbleToMove()
    {
        SetTableColorToBlue();
        if(!isTableAbleToMove)
        {
            SendNotification($"A table in \"{tablePlanName}\" table plan can move");
        }
        isTableAbleToMove = true;
        UpdateFreePlansDataInInfoCanvas();
    }

    /// <summary>
    /// Table plans that this table wants to move to
    /// </summary>
    /// <returns> Table plans</returns>
    public List<GameObject> ReturnAListOfTablePlansThatTableWantsToMoveTo()
    {
        return tablePlansThatTableWantsToMoveTo;
    }

    /// <summary>
    /// Here we set the main controller gameobject to communicate with all the other scripts
    /// </summary>
    private void Start()
    {
        mainController = GameObject.Find("MainHostController");
        controller = GameObject.Find("Controller");
    }

    /// <summary>
    /// Returns table plan name this table belongs to
    /// </summary>
    /// <returns> Table plan name</returns>
    public string GetTablePlanName()
    {
        return tablePlanName;
    }

    /// <summary>
    /// Returs the time the table was seated
    /// </summary>
    /// <returns> The time the table was booked</returns>
    public DateTime GetTimeSeated()
    {
        return timeSeated;
    }

    /// <summary>
    /// Returns the pax at the table
    /// </summary>
    /// <returns> The pax at the table</returns>
    public int GetPax()
    {
        return pax;
    }

    /// <summary>
    /// Returns the projected leaving time of the table
    /// </summary>
    /// <returns>The projected table leaving time</returns>
    public DateTime GetProjectedLeavingTime()
    {
        return projectedLeavingTime;
    }

    /// <summary>
    /// Here we return the table projected leaving time;
    /// </summary>
    /// <returns></returns>
    public DateTime ProjectedTableLeavingTime()
    {
        return projectedLeavingTime;
    }

    /// <summary>
    /// Return table status (free/false or not free/true)
    /// </summary>
    /// <returns></returns>
    public bool ReturnIfTheTableIsTaken()
    {
        return isTableTaken;
    }

    /// <summary>
    /// Move another table to this one
    /// </summary>
    /// <param name="senderTimeSeated"> The time the other table was seated</param>
    /// <param name="senderPax"> The pax the other table had</param>
    /// <param name="senderProjectedLeavingTime"> Projected leaving time of the other table</param>
    public void MoveToThisTable(DateTime senderTimeSeated, int senderPax, DateTime senderProjectedLeavingTime)
    {
        isTableTaken = true;
        isTableMarked = false;
        SetTableColorToRed();
        timeSeated = senderTimeSeated;
        pax = senderPax;
        projectedLeavingTime = senderProjectedLeavingTime;
    }

    /// <summary>
    /// Here we merge 2 table data
    /// </summary>
    public void MergeTheeseTables(DateTime senderTimeSeated, int senderPax, DateTime senderProjectedLeavingTime)
    {
        pax += senderPax;
        if(senderTimeSeated < timeSeated)
        {
            timeSeated = senderTimeSeated;
        }
        if(projectedLeavingTime < senderProjectedLeavingTime)
        {
            projectedLeavingTime = senderProjectedLeavingTime;
        }
    }

    /// <summary>
    /// Here we clear the table data after moving
    /// </summary>
    public void ClearThisTable()
    {
        isTableTaken = false;
        isTableMarked = false;
        SetTableColorToGreen();
        if(tableWantsToMove)
        {
            tableWantsToMove = false;
            mainController.GetComponent<MainHostController>().RemoveFromTablesThatWantToMoveList(this.gameObject);
            mainController.GetComponent<MainHostController>().GetDisplayTableInfoCanvas().GetComponent<TableInfoCanvasController>().UpdateMovingList(null);
            tablePlansThatTableCanMoveTo = null;
            mainController.GetComponent<MainHostController>().GetDisplayTableInfoCanvas().SetActive(false);
        }
    }

    /// <summary>
    /// Here we send info on click to the appropriate canvas depending if the table is taken
    /// </summary>
    public void SendInfo()
    {
        if (mainController.GetComponent<MainHostController>().IsTableMoving())
        {
            if (isTableTaken)
            {
                mainController.GetComponent<MainHostController>().TableIsTakenForMoving(this.gameObject);
            }
            else
            {
                mainController.GetComponent<MainHostController>().TableIsFreeForMoving(this.gameObject);
            }
        }
        else if (isTableTaken)
        {
            mainController.GetComponent<MainHostController>().GetDisplayTableInfoCanvas().GetComponent<TableInfoCanvasController>().ReceiveData(pax, timeSeated, projectedLeavingTime, this.gameObject, tablePlansThatTableCanMoveTo);
        }
        else
        {
            mainController.GetComponent<MainHostController>().ReceiveNewTableInfo(this.gameObject);
        }
    }

    /// <summary>
    /// Here we set the table as not in use
    /// </summary>
    public void CloseTable()
    {
        SaveTableData();
        isTableTaken = false;
        if (tableWantsToMove)
        {
            tableWantsToMove = false;
            mainController.GetComponent<MainHostController>().RemoveFromTablesThatWantToMoveList(this.gameObject);
            mainController.GetComponent<MainHostController>().GetDisplayTableInfoCanvas().GetComponent<TableInfoCanvasController>().UpdateMovingList(null);
            tablePlansThatTableCanMoveTo = null;
        }
        mainController.GetComponent<MainHostController>().UpdateCurrentPax(-pax);
        pax = 0;
        SetTableColorToGreen();
    }

    /// <summary>
    /// Saves table stay data
    /// </summary>
    private void SaveTableData()
    {
        int timeStayed;
        TimeSpan stayTime = DateTime.Now - timeSeated;

        double temp = Math.Round(stayTime.TotalMinutes);

        timeStayed = Convert.ToInt32(temp);

        CollectData.AddTableStay(timeStayed, pax);
    }

    /// <summary>
    /// Here we receive updated table information
    /// </summary>
    /// <param name="newPax"> The new pax of the table</param>
    public void ReceiveUpdatePax(int newPax)
    {
        projectedLeavingTime = projectedLeavingTime.AddMinutes(SettingsController.GetProjectedTableStayPerPersonTime() * (newPax - pax) + 1);// we are adding one because the user would see that its the same time of leaving than it is shown
        pax = newPax;
    }

    /// <summary>
    /// Here we receive the pax for new table
    /// </summary>
    /// <param name="receivedPax"> the Pax for the table</param>
    public void ReceiveNewTableData(int receivedPax)
    {
        isTableTaken = true;
        isTableMarked = false;
        timeSeated = DateTime.Now;
        pax = receivedPax;
        projectedLeavingTime = timeSeated.AddMinutes(SettingsController.GetProjectedTableStayPerPersonTime() * pax);       // Here we multiply the table stay time with pax count because bigger groups stay longer
        SetTableColorToRed();
    }

    /// <summary>
    /// Marks a table that is not taken
    /// </summary>
    public void MarkATable()
    {
        if(isTableMarked)
        {
            SetTableColorToGreen();
            isTableMarked = false;
        }
        else
        {
            SetTableColorToYellow();
            isTableMarked = true;
        }
    }

    /// <summary>
    /// Red is for table is taken
    /// </summary>
    private void SetTableColorToRed()
    {
        this.gameObject.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color32(200, 50, 50, 255);
        controller.GetComponent<Controller>().GetTableImageByTableGameobject(this.gameObject.transform.parent.parent.name, this.gameObject.name).GetComponent<IsTableImageFree>().SetTableColorToRed();
    }

    /// <summary>
    /// Green is for table is free
    /// </summary>
    private void SetTableColorToGreen()
    {
        this.gameObject.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color32(50, 200, 50, 255);
        controller.GetComponent<Controller>().GetTableImageByTableGameobject(this.gameObject.transform.parent.parent.name, this.gameObject.name).GetComponent<IsTableImageFree>().SetTableColorToGreen();

    }

    /// <summary>
    /// Oragne is for table that wants to move but doesnt have anywhere to move
    /// </summary>
    private void SetTableColorToOrange()
    {
        this.gameObject.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color32(200, 100, 50, 255);
        controller.GetComponent<Controller>().GetTableImageByTableGameobject(this.gameObject.transform.parent.parent.name, this.gameObject.name).GetComponent<IsTableImageFree>().SetTableColorToOrange();
    }

    /// <summary>
    /// Blue is for table that wants to move and we have a space for it to move
    /// </summary>
    private void SetTableColorToBlue()
    {
        this.gameObject.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color32(50, 50, 200, 255);
        controller.GetComponent<Controller>().GetTableImageByTableGameobject(this.gameObject.transform.parent.parent.name, this.gameObject.name).GetComponent<IsTableImageFree>().SetTableColorToBlue();
    }

    /// <summary>
    /// Blue is for table that wants to move and we have a space for it to move
    /// </summary>
    private void SetTableColorToYellow()
    {
        this.gameObject.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color32(255, 255, 100, 255);
        controller.GetComponent<Controller>().GetTableImageByTableGameobject(this.gameObject.transform.parent.parent.name, this.gameObject.name).GetComponent<IsTableImageFree>().SetTableColorToYellow();
    }

    /// <summary>
    /// Receive the plan list that the table wants to move to
    /// </summary>
    /// <param name="plansList"></param>
    public void ReceiveListOfTablePlansToMoveTo(List<GameObject> plansList)
    {
        tablePlansThatTableWantsToMoveTo = plansList;
        tableWantsToMove = true;
        SetTableColorToOrange();
        mainController.GetComponent<MainHostController>().AddToTablesThatWantToMoveList(this.gameObject);
    }
}
