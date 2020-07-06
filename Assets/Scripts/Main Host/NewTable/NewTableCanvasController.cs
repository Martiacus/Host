using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewTableCanvasController : MonoBehaviour
{
    private GameObject table;                       // The table that is requesting the data
    public Text paxInputField;              // Here we have the inputfield that displays the current PAx
    public MainHostController controller;   // Our main controller to send the saved pax count

    int pax;                                // We use the pax = 0 because we know that it will always start at 0

    /// <summary>
    /// Marks or unmarks a table
    /// </summary>
    public void MarkATable()
    {
        table.GetComponent<TableButtonInfo>().MarkATable();
    }

    /// <summary>
    /// Here we receive the info who the sender is so we know where to send data back
    /// </summary>
    /// <param name="sender"></param>
    public void ReceiveTableInfo(GameObject sender)
    {
        ResetData();
        table = sender;
    }

    /// <summary>
    /// Increases pax count by 1
    /// </summary>
    public void IncreasePaxCountByOne()
    {
        pax += 1;
        SetPaxCountToText(pax);
    }

    /// <summary>
    /// Decreases pax count by 1, but it cannto decrease it bellow zero
    /// </summary>
    public void DecreasePaxCountByOne()
    {
        if (pax > 1)
        {
            pax -= 1;
            SetPaxCountToText(pax);
        }
        else
        {
            pax = 1;
            SetPaxCountToText(pax);
        }
    }

    /// <summary>
    /// Here we reset the data after finishing the data input for new table
    /// </summary>
    private void ResetData()
    {
        pax = 1;
        SetPaxCountToText(pax);
        table = null;
    }

    /// <summary>
    /// Here we send the data back to the receiver;
    /// </summary>
    public void SendDataBack()
    {
        table.GetComponent<TableButtonInfo>().ReceiveNewTableData(pax);
        controller.UpdateCurrentPax(pax);
        this.gameObject.SetActive(false);   // Here we disable this canvas and waiting for new data
    }

    /// <summary>
    /// Here we set the count number to a text format and display it
    /// </summary>
    /// <param name="count">The number being displayed</param>
    private void SetPaxCountToText(int count)
    {
        paxInputField.text = count.ToString();
    }

    /// <summary>
    /// Here we save the table or we ask if the person is seating ofr a booking if there is one on the table
    /// </summary>
    public void SaveNewTable()
    {
        SendDataBack();
    }

    /// <summary>
    /// Here we set the pax count to a specific number
    /// </summary>
    public void SetPaxToOne()
    {
        pax = 1;
        SetPaxCountToText(pax);
    }
    public void SetPaxToTwo()
    {
        pax = 2;
        SetPaxCountToText(pax);
    }
    public void SetPaxToThree()
    {
        pax = 3;
        SetPaxCountToText(pax);
    }
    public void SetPaxToFour()
    {
        pax = 4;
        SetPaxCountToText(pax);
    }
    public void SetPaxToFive()
    {
        pax = 5;
        SetPaxCountToText(pax);
    }
    public void SetPaxToSix()
    {
        pax = 6;
        SetPaxCountToText(pax);
    }
    public void SetPaxToSeven()
    {
        pax = 7;
        SetPaxCountToText(pax);
    }
    public void SetPaxToEight()
    {
        pax = 8;
        SetPaxCountToText(pax);
    }
}
