using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainHostController : MonoBehaviour
{
    public GameObject createNewTableCanvas; // Canvas we are contacting for creating a new table
    public GameObject displayTableInfo;     // The canvas where we display the table properties
    private int totalPax;                    // The total pax currently inside
    private int currentPax;                  // Current pax inside
    private int peakPax;                     // Max pax today
    private DateTime peakPaxTime;            // here we save the time maximum pax was reached
    public int checkFreeTablesRepeatTime;   // Here we set the free table stay time check script repetion rate

    public List<GameObject> tables;         // Here we store all of our tables
    public List<GameObject> tablesThatWantToMove;   // Here we store tables that want to move

    private bool tableIsMoving;              // Here we declare if the table is moving so other tables know that im moving
    private GameObject movingTable;          // This is the table that is moving
    public GameObject movingTableMessageCanvas; // This is the canvas for confirming the move
    private GameObject movingToTable;        // This is the table that is being moved to
    public GameObject cancelMoveButton;     // Cancel move button
    public GameObject notificationsCanvas;  // Notifications canvas

    public List<String> notificationsQueue;    // Here we have a list of notifications to displayed and not closed yet

    public GameObject bookings;             // This is our booking button that we will enable if we have more than one booking

    private void OnEnable()
    {
        totalPax = 0;
        currentPax = 0;
        peakPax = 0;
        peakPaxTime = DateTime.Now;
        notificationsQueue = new List<string>();
        InvokeRepeating("CheckTablesThatWantToMove", checkFreeTablesRepeatTime, checkFreeTablesRepeatTime);
        if (SettingsController.GetExtraTableStayTime() > 0)
        {
            InvokeRepeating("ExtendTableStayTime", checkFreeTablesRepeatTime, checkFreeTablesRepeatTime);
        }
        EnableBookings();
        StartSaving();
    }

    public void SavePaxData()
    {
        CollectData.SetPeakPax(peakPax);
        CollectData.SetPeakPaxTime(peakPaxTime);
        CollectData.SetTotalPax(totalPax);
    }

    /// <summary>
    /// We want to save data every 30 minutes on 00 and 30th minute so we find when to start the script to start saving correctly
    /// </summary>
    private void StartSaving()
    {
        int tempStartTime = 1800; // seconds

        if (DateTime.Now.Minute >= 30)
        {
            tempStartTime += 1800;
        }

        int startTime = tempStartTime - (DateTime.Now.Minute * 60 + DateTime.Now.Second);

        InvokeRepeating("SaveCurrentPaxCountCollectedData", startTime, 1800);
    }

    /// <summary>
    /// Saves our current relevant colleted data
    /// </summary>
    private void SaveCurrentPaxCountCollectedData()
    {
        CollectData.AddPaxCount(DateTime.Now, currentPax, totalPax);
    }

    /// <summary>
    /// Enables bookings button
    /// </summary>
    private void EnableBookings()
    {
        if(GameObject.Find("Controller").GetComponent<Controller>().GetBookingsCount() > 0)
        {
            bookings.SetActive(true);
        }
        else if(bookings.activeSelf)
        {
            bookings.SetActive(false);
        }
    }

    /// <summary>
    /// Extends all table times that have passed their projected leaving time
    /// </summary>
    private void ExtendTableStayTime()
    {
        foreach(GameObject table in tables)
        {
            if (table.GetComponent<TableButtonInfo>().ReturnIfTheTableIsTaken())
            {
                if (table.GetComponent<TableButtonInfo>().GetProjectedLeavingTime() < DateTime.Now)
                {
                    table.GetComponent<TableButtonInfo>().IncreaseTableStayTime(SettingsController.GetExtraTableStayTime());
                }
            }
        }
    }

    /// <summary>
    /// Checks if there are any tables available in any of the selected table plans
    /// </summary>
    private void CheckTablesThatWantToMove()
    {
        int tablePlanCountThatIsAbleToMoveTo;
        List<GameObject> tempPlansToMoveTo;
        List<string> tablePlansThatTheTableCanMoveTo;
        List<GameObject> usedPlans;

        if (tablesThatWantToMove.Count != 0)
        {
            foreach (GameObject table in tablesThatWantToMove)
            {
                tablePlansThatTheTableCanMoveTo = new List<string>();
                tempPlansToMoveTo = table.GetComponent<TableButtonInfo>().ReturnAListOfTablePlansThatTableWantsToMoveTo();
                tablePlanCountThatIsAbleToMoveTo = 0;
                usedPlans = new List<GameObject>();
                bool used = false;

                foreach (GameObject singleTableFromAllTables in tables)
                {
                    if (!singleTableFromAllTables.GetComponent<TableButtonInfo>().ReturnIfTheTableIsTaken())
                    {

                        foreach (GameObject tempPlan in tempPlansToMoveTo)
                        {
                            used = false;
                            foreach (GameObject usedPlan in usedPlans)
                            {
                                if (!used && tempPlan == usedPlan)
                                {
                                    used = true;
                                }
                            }

                            if (!used)
                            {
                                if (tempPlan.name == singleTableFromAllTables.GetComponent<TableButtonInfo>().GetTablePlanName())
                                {
                                    tablePlansThatTheTableCanMoveTo.Add(tempPlan.name);
                                    tablePlanCountThatIsAbleToMoveTo++;
                                    usedPlans.Add(tempPlan);
                                }
                            }
                        }
                    }
                }
                if (tablePlanCountThatIsAbleToMoveTo > 0)
                {
                    table.GetComponent<TableButtonInfo>().SetTablePlansListThatTableIsAbleToMoveTo(tablePlansThatTheTableCanMoveTo);
                    table.GetComponent<TableButtonInfo>().SetTableAsAbleToMove();
                }
                else
                {
                    table.GetComponent<TableButtonInfo>().SetTableAsUnableToMove();
                }
            }
        }
    }

    /// <summary>
    /// Returns the information canvas
    /// </summary>
    /// <returns>Information canvas</returns>
    public GameObject GetInfoCanvas()
    {
        return displayTableInfo;
    }

    /// <summary>
    /// Remove a table from a list of tables that want to move
    /// </summary>
    /// <param name="table"> Table to be removed</param>
    public void RemoveFromTablesThatWantToMoveList(GameObject table)
    {
        tablesThatWantToMove.Remove(table);
    }

    /// <summary>
    /// Add a table to a list of tables that want to move
    /// </summary>
    /// <param name="table"> Table to be added</param>
    public void AddToTablesThatWantToMoveList(GameObject table)
    {
        tablesThatWantToMove.Add(table);
    }

    /// <summary>
    /// Adds a notification text to notifications list and starts the notifications canvas
    /// </summary>
    /// <param name="text"> The notification text</param>
    public void AddNotification(string text)
    {
        notificationsQueue.Add(text);
        StartNotifications();
    }

    /// <summary>
    /// Starts notifications canvas it is inactive
    /// </summary>
    private void StartNotifications()
    {
        if(!notificationsCanvas.activeSelf)
        {
            notificationsCanvas.SetActive(true);
            notificationsCanvas.GetComponent<NotificationsController>().StartNotifications(notificationsQueue[0]);
        }
    }


    /// <summary>
    /// Decreases the notification queue and returns a new notification if the list is not empty
    /// </summary>
    /// <returns></returns>
    public void DecreaseNotificationQueue()
    {
        notificationsQueue.RemoveAt(0);
        if(notificationsQueue.Count == 0)
        {
            notificationsCanvas.SetActive(false);
        }
        else
        {
            notificationsCanvas.GetComponent<NotificationsController>().StartNotifications(notificationsQueue[0]);
        }
    }

    /// <summary>
    /// Here we set the data for canceling the moving process
    /// </summary>
    public void CancelMove()
    {
        tableIsMoving = false;
        movingToTable = null;
        movingToTable = null;
        cancelMoveButton.SetActive(false);
    }

    /// <summary>
    /// Here we set the data for a new table
    /// </summary>
    public void MoveToFreeTable()
    {
        movingToTable.GetComponent<TableButtonInfo>().MoveToThisTable(
            movingTable.GetComponent<TableButtonInfo>().GetTimeSeated(),
            movingTable.GetComponent<TableButtonInfo>().GetPax(),
            movingTable.GetComponent<TableButtonInfo>().GetProjectedLeavingTime()
            );
        movingTable.GetComponent<TableButtonInfo>().ClearThisTable();
        CancelMove();   // We use this script because it pretty much does everything we need to reset all data
    }

    /// <summary>
    /// Here we set the data for merging the two tables
    /// </summary>
    public void MergeTwoTables()
    {
        movingToTable.GetComponent<TableButtonInfo>().MergeTheeseTables(
            movingTable.GetComponent<TableButtonInfo>().GetTimeSeated(),
            movingTable.GetComponent<TableButtonInfo>().GetPax(),
            movingTable.GetComponent<TableButtonInfo>().GetProjectedLeavingTime()
            );
        movingTable.GetComponent<TableButtonInfo>().ClearThisTable();
        CancelMove();   // We use this script because it pretty much does everything we need to reset all data
    }

    /// <summary>
    /// Here we set the table that is free to move to
    /// </summary>
    /// <param name="freeTable"></param>
    public void TableIsFreeForMoving(GameObject freeTable)
    {
        movingTableMessageCanvas.SetActive(true);
        movingToTable = freeTable;
        movingTableMessageCanvas.GetComponent<MovingTableConfirmationCanvas>().TableIsFree();
    }

    /// <summary>
    /// Here we enable a message canvas for merging if the table is taken
    /// </summary>
    /// <param name="takenTable"> The table that is already taken</param>
    public void TableIsTakenForMoving(GameObject takenTable)
    {
        if(takenTable != movingTable)
        {
            movingTableMessageCanvas.SetActive(true);
            movingToTable = takenTable;
            movingTableMessageCanvas.GetComponent<MovingTableConfirmationCanvas>().TableIsTaken();
        }
    }

    /// <summary>
    /// Here we return if the table is moving
    /// </summary>
    /// <returns>True if table is moving, false if no table is moving</returns>
    public bool IsTableMoving()
    {
        return tableIsMoving;
    }

    /// <summary>
    /// Here we call this function if the table is moving and set the parameters for moving table
    /// </summary>
    /// <param name="table"> The table that is moving</param>
    public void TableIsMoving(GameObject table)
    {
        tableIsMoving = true;
        movingTable = table;
        cancelMoveButton.SetActive(true);
    }

    /// <summary>
    /// Here we return the table gameobject that is moving
    /// </summary>
    /// <returns> Table gameobject</returns>
    public GameObject GetMovingTable()
    {
        return movingTable;
    }

    /// <summary>
    /// Here we clear old tables data when closing
    /// </summary>
    public void ClearOldTables()
    {
        tables.Clear();
    }

    /// <summary>
    /// Returns current pax count inside
    /// </summary>
    /// <returns> Current pax count</returns>
    public int GetCurrentPaxCount()
    {
        return currentPax;
    }

    /// <summary>
    /// Here we update the maximum PAX today
    /// </summary>
    private void UpdatePeakPax()
    {
        if (peakPax < currentPax)
        {
            peakPax = currentPax;
            peakPaxTime = DateTime.Now;
        }
    }

    /// <summary>
    /// Here we get the information canvas to send the data to
    /// </summary>
    /// <returns> The table information canvas</returns>
    public GameObject GetDisplayTableInfoCanvas()
    {
        displayTableInfo.SetActive(true);
        return displayTableInfo;
    }

    /// <summary>
    /// Here we give the current PAX Seated
    /// </summary>
    /// <returns>Current PAX seated</returns>
    public int CurrentPax()
    {
        return currentPax;
    }

    /// <summary>
    /// Here we give the total PAX today
    /// </summary>
    /// <returns>Total PAX today</returns>
    public int TotalPax()
    {
        return totalPax;
    }

    /// <summary>
    /// Here we return the time the next table is going to be free ( projected time )
    /// </summary>
    /// <returns> The lowest time to next free table</returns>
    public string ReturnNextFreeTableTime()
    {
        DateTime temptime = DateTime.Now;
        temptime = temptime.AddDays(1);
        DateTime check = temptime;

        foreach (GameObject table in tables)
        {
            DateTime tabletime = table.GetComponent<TableButtonInfo>().ProjectedTableLeavingTime();
            if (tabletime != null && temptime > tabletime && tabletime > DateTime.Now)
            {
                temptime = table.GetComponent<TableButtonInfo>().ProjectedTableLeavingTime();
            }
        }
        if (temptime == check)
        {
            return "N/A";
        }
        else
        {
            return temptime.ToShortTimeString();
        }
    }

    /// <summary>
    /// Here we find all tables and assign them to our list
    /// </summary>
    public void AddTableToAllTableList(GameObject table)
    {
        tables.Add(table);
    }

    /// <summary>
    /// Returns taken table count
    /// </summary>
    /// <returns> Taken table count</returns>
    public int ReturnTakenTableCount()
    {
        int count = 0;

        foreach (GameObject table in tables)
        {
            if (table.GetComponent<TableButtonInfo>().ReturnIfTheTableIsTaken())
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// Counts all free tables in the canvas
    /// </summary>
    /// <returns></returns>
    public int ReturnFreeTableCount()
    {
        int count = 0;

        foreach (GameObject table in tables)
        {
            if (!table.GetComponent<TableButtonInfo>().ReturnIfTheTableIsTaken())
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// This is used to enable the canvas for new table and who is requesting the data
    /// </summary>
    /// <param name="senderTable">The table who is sending the data</param>
    public void ReceiveNewTableInfo(GameObject senderTable)
    {
        createNewTableCanvas.SetActive(true);
        createNewTableCanvas.GetComponent<NewTableCanvasController>().ReceiveTableInfo(senderTable);
    }

    /// <summary>
    /// Here we update the total pax
    /// </summary>
    /// <param name="count">the pax we are updating with</param>
    private void UpdateTotalPax(int count)
    {
        totalPax += count;
    }

    /// <summary>
    /// Here we display the currentPax
    /// </summary>
    /// <param name="count"> the ammount we are updating with</param>
    public void UpdateCurrentPax(int count)
    {
        currentPax += count;

        // Here if the count is more than 0 we also update total Pax
        if (count > 0)
        {
            UpdateTotalPax(count);
        }

        UpdatePeakPax();
    }
}
