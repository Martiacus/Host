using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CreateBookingsButtons : MonoBehaviour
{
    public Controller controller;
    public GameObject bookingPrefab;

    private int currentBookingNo;

    /// <summary>
    /// Requests bookings information and to create the bookigns buttons
    /// </summary>
    public void RequestBookings()
    {
        ClearAllCurrentBookingsButtons();
        ResetBookingNo();
        controller.GetBookingsForBookingsCanvas();
    }

    /// <summary>
    /// Resets the booking number
    /// </summary>
    public void ResetBookingNo()
    {
        currentBookingNo = 0;
    }

    /// <summary>
    /// Receives the total bookings count so we can adjust the size of the content canvas
    /// </summary>
    /// <param name="bookingCount"> Count of bookings</param>
    public void ReceiveBookingsCount(int bookingCount)
    {
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (bookingCount) * 100);
    }

    /// <summary>
    /// Receives a single booking information and sends it to another method to create everything
    /// </summary>
    /// <param name="time"> Time of booking hours and minutes</param>
    /// <param name="nameBooked"> Name of the person who booked</param>
    /// <param name="pax"> Pax of booking</param>
    /// <param name="info"> Any extra information if available</param>
    public void ReceiveABooking(DateTime time, string nameBooked, int pax, string info)
    {
        CreateABooking(time, nameBooked, pax, info);
    }

    /// <summary>
    /// Creates the booking button with the given data
    /// </summary>
    /// <param name="time"> Time of booking hours and minutes</param>
    /// <param name="nameBooked"> Name of the person who booked</param>
    /// <param name="pax"> Pax of booking</param>
    /// <param name="info"> Any extra information if available</param>
    private void CreateABooking(DateTime time, string nameBooked, int pax, string info)
    {
        GameObject tempGameobject = Instantiate(bookingPrefab, this.gameObject.transform);
        tempGameobject.transform.localPosition = new Vector3(0, 50 - 100 * (currentBookingNo + 1), 0);
        tempGameobject.GetComponent<RectTransform>().offsetMax = new Vector2(0, tempGameobject.GetComponent<RectTransform>().offsetMax.y);
        tempGameobject.GetComponent<RectTransform>().offsetMin = new Vector2(0, tempGameobject.GetComponent<RectTransform>().offsetMin.y);
        tempGameobject.name = nameBooked;
        tempGameobject.tag = "BookingCanvasButton";
        tempGameobject.transform.GetChild(1).GetComponent<Text>().text = time.ToShortTimeString();
        tempGameobject.transform.GetChild(2).GetComponent<Text>().text = nameBooked;
        tempGameobject.transform.GetChild(3).GetComponent<Text>().text = pax.ToString();
        tempGameobject.transform.GetChild(4).GetComponent<Text>().text = info;

        currentBookingNo++;
    }


    /// <summary>
    /// Deletes all bookings buttons
    /// </summary>
    public void ClearAllCurrentBookingsButtons()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("BookingCanvasButton");

        foreach(GameObject button in buttons)
        {
            Destroy(button);
        }

        ResetBookingNo();
    }
}
