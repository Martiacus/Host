using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SaveNewBooking : MonoBehaviour
{
    public Controller controller;

    public Text hour;
    public Text minute;
    public InputField bookingName;
    public Text pax;
    public InputField extraInfo;
    public GameObject ErrorCanvas;
    public GameObject cancelButton;

    /// <summary>
    /// Saves the data to the controller
    /// </summary>
    public void Save()
    {
        if(bookingName.text == null || bookingName.text == "")
        {
            ErrorCanvas.SetActive(true);
            ErrorCanvas.GetComponent<ErrorStateCanvas>().ErrorPleaseSelectBookingName();
        }
        else
        {
            controller.AddBooking(GetTime(), bookingName.text, int.Parse(pax.text), extraInfo.text);
            ClearData();
            cancelButton.GetComponent<DisableCanvas>().OnClick();
        }
    }

    /// <summary>
    /// Clears the data fields
    /// </summary>
    public void ClearData()
    {
        hour.text = "0";
        minute.text = "0";
        bookingName.text = "";
        pax.text = "1";
        extraInfo.text = null;
    }

/// <summary>
/// Converts 2 strings Hour and Minute to a datetime format
/// </summary>
/// <returns> Booking time</returns>
private DateTime GetTime()
    {
        DateTime temptime = DateTime.Now;

        //Here we set the day. we check if the time is already in another day or still the same day
        if (temptime.Hour == int.Parse(hour.text))
        {
            if (temptime.Minute >= int.Parse(minute.text))
            {
                temptime = temptime.AddDays(1);
            }
        }
        else if (temptime.Hour > int.Parse(hour.text))
        {
            temptime = temptime.AddDays(1);
        }

        // Here we check the hour and set it accordingly
        if (temptime.Hour > int.Parse(hour.text))
        {
            temptime = temptime.AddHours(-(temptime.Hour - int.Parse(hour.text)));
        }
        else
        {
            temptime = temptime.AddHours(int.Parse(hour.text) - temptime.Hour);
        }

        //Here we check the minute and set it accordingly
        if (temptime.Minute > int.Parse(minute.text))
        {
            temptime = temptime.AddMinutes(-(temptime.Minute - int.Parse(minute.text)));
        }
        else
        {
            temptime = temptime.AddMinutes(int.Parse(minute.text) - temptime.Minute);
        }

        return temptime;
    }
}