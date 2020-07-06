using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TableInfoCanvasController : MonoBehaviour
{
    private int pax;
    private DateTime timeSeated;
    private DateTime projectedLeavingTime;
    private List<String> tablePlansToMoveTo;

    public Text movingToTablesCanvas;
    public Text paxStatus;
    public Text editPaxStatus;
    public Text projectedLeavingTimeStatus;
    public Text TimeSeatedStatus;
    private GameObject table;
    public MainHostController mainHostController;
    public GameObject closeTableConfirmation;
    public GameObject tableWantsToMoveCanvas;

    /// <summary>
    /// Receive data without table booking
    /// </summary>
    /// <param name="paxCount"> Pax that is seated on the table</param>
    /// <param name="timeTableWasSeated"> The time the table was seated</param>
    /// <param name="projectedTableLeavingTime"> The time projected for table to leave</param>
    public void ReceiveData(int paxCount, DateTime timeTableWasSeated, DateTime projectedTableLeavingTime, GameObject sender, List<String> movingToPlans)
    {
        pax = paxCount;
        timeSeated = timeTableWasSeated;
        projectedLeavingTime = projectedTableLeavingTime;
        table = sender;
        tablePlansToMoveTo = new List<string>();
        tablePlansToMoveTo = movingToPlans;
        SetDataWithoutBooking();
    }

    /// <summary>
    /// Activates table wants to move canvas
    /// </summary>
    /// <param name="table"> Table that wants to move</param>
    public void TableWantsToMove()
    {
        tableWantsToMoveCanvas.SetActive(true);
        tableWantsToMoveCanvas.GetComponent<TableWantsToMoveController>().ReceiveTable(table);
    }

    /// <summary>
    /// Here we send table moving data to main host controller so it can comunicate with other tables
    /// </summary>
    public void SendTableMovingInfo()
    {
        mainHostController.TableIsMoving(table);
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Here we update the pax data displayed
    /// </summary>
    private void UpdatePaxData()
    {
        paxStatus.text = pax.ToString();
        editPaxStatus.text = pax.ToString();
    }

    /// <summary>
    /// Here we set the data fields with data that do not include booking data
    /// </summary>
    private void SetDataWithoutBooking()
    {
        paxStatus.text = pax.ToString();
        editPaxStatus.text = pax.ToString();
        projectedLeavingTimeStatus.text = projectedLeavingTime.ToShortTimeString();
        TimeSeatedStatus.text = timeSeated.ToShortTimeString();
        SetMovingToText();
    }

    /// <summary>
    /// Updates the list for moving tables
    /// </summary>
    /// <param name="updatedList"></param>
    public void UpdateMovingList(List<String> updatedList)
    {
        tablePlansToMoveTo = updatedList;
        SetMovingToText();
    }

    /// <summary>
    /// Sets the text for moving tables to free Table plans
    /// </summary>
    private void SetMovingToText()
    {
        string tempText = "";

        if (tablePlansToMoveTo != null)
        {
            if (tablePlansToMoveTo.Count > 0)
            {
                if (tablePlansToMoveTo.Count == 1)
                {
                    tempText = "Free table in table plan:";
                }
                else
                {
                    tempText = "Free table in table plans:";
                }
                foreach (string temp in tablePlansToMoveTo)
                {
                    tempText += $"\n{temp}";
                }
            }
        }


        movingToTablesCanvas.text = tempText;
    }

    /// <summary>
    /// Updates our display of projected leaving time
    /// </summary>
    private void UpdateTableLeavingTime()
    {
        projectedLeavingTimeStatus.text = projectedLeavingTime.ToShortTimeString();
    }

    /// <summary>
    /// Here we increase pax count by one
    /// </summary>
    public void IncreasePaxCountByOne()
    {
        pax++;
        mainHostController.UpdateCurrentPax(1);
        table.GetComponent<TableButtonInfo>().ReceiveUpdatePax(pax);
        projectedLeavingTime = projectedLeavingTime.AddMinutes(SettingsController.GetProjectedTableStayPerPersonTime());
        UpdateTableLeavingTime();
        UpdatePaxData();
    }

    /// <summary>
    /// Here we decrease pax count by 1 but if pax is already one that means there is no1 at the table and we call a canvas that confirms if we want to close the table
    /// </summary>
    public void DecreasePaxCountByOne()
    {
        if(pax > 1)
        {
            pax--;
            mainHostController.UpdateCurrentPax(-1);
            table.GetComponent<TableButtonInfo>().ReceiveUpdatePax(pax);
            projectedLeavingTime = projectedLeavingTime.AddMinutes(-SettingsController.GetProjectedTableStayPerPersonTime());
            UpdateTableLeavingTime();
            UpdatePaxData();
        }
        else
        {
            closeTableConfirmation.SetActive(true);
        }
    }

    /// <summary>
    /// Here we set the table closing confirmation canvas to on to confirm the closing of the table
    /// </summary>
    public void CloseTableConfirmation()
    {
        closeTableConfirmation.SetActive(true);
    }

    /// <summary>
    /// Here we close the table
    /// </summary>
    public void CloseTable()
    {
        table.GetComponent<TableButtonInfo>().CloseTable();
        this.gameObject.SetActive(false);
    }
}