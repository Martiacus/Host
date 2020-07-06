using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DisplayTotalDataController : MonoBehaviour
{
    public MainHostController mainHostController;       // The canvas that stores all table plans and tables
    public Text totalPax;                               // Displays our total pax count; 
    public Text currentPax;                             // Displays our current pax count
    public Text freeTableCount;                         // Displays current free tables;
    public Text nextFreeTable;                          // Displays next free table time

    void Update()
    {
        DisplayCurrentPAX();
        DisplayTotalPAX();
        DisplayFreeTableCount();
        DisplayNextFreeTableTime();
    }

    /// <summary>
    /// Here we display the current PAX seated inside
    /// </summary>
    private void DisplayCurrentPAX()
    {
        currentPax.text = mainHostController.GetCurrentPaxCount().ToString();
    }

    /// <summary>
    /// Here we display the total PAX today
    /// </summary>
    private void DisplayTotalPAX()
    {
        totalPax.text = mainHostController.TotalPax().ToString();
    }

    /// <summary>
    /// Here we display total free table count
    /// </summary>
    private void DisplayFreeTableCount()
    {
        freeTableCount.text = mainHostController.ReturnFreeTableCount().ToString();
    }


    /// <summary>
    /// Here we display the next projected free table time if we have no tables available now
    /// </summary>
    private void DisplayNextFreeTableTime()
    {
        if (freeTableCount.text != null)
        {
            if (int.Parse(freeTableCount.text) == 0)
            {
                nextFreeTable.text = mainHostController.ReturnNextFreeTableTime();
            }
            else
            {
                nextFreeTable.text = "N/A";
            }
        }
        else
        {
            nextFreeTable.text = "N/A";
        }
    }
}
