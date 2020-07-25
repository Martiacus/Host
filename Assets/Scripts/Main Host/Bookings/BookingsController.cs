using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BookingsController : MonoBehaviour
{
    public Controller controller;
    public GameObject bookingsList;

    public GameObject bookingButtonPrefab;
    public GameObject bookingInfoPrefab;

    public GameObject extraInfoText;

    private List<Bookings> bookings;

    private int currentBookingNo;

    public MainHostController mainHostController;

    class Bookings
    {
        DateTime timeBooked;
        string nameBooking;
        int paxBooking;
        string extraInformation;

        /// <summary>
        /// Sets the data for a single booking
        /// </summary>
        /// <param name="time"> Time of booking hours and minutes</param>
        /// <param name="nameBooked"> Name of the person who booked</param>
        /// <param name="pax"> Pax of booking</param>
        /// <param name="info"> Any extra information if available</param>
        public Bookings(DateTime time, string nameBooked, int pax, string info)
        {
            timeBooked = time;
            nameBooking = nameBooked;
            paxBooking = pax;
            extraInformation = info;
        }

        /// <summary>
        /// Retruns the time the booking was placed
        /// </summary>
        /// <returns> Booking time</returns>
        public DateTime GetTimeBooked()
        {
            return timeBooked;
        }

        /// <summary>
        /// Return the name the booking is under
        /// </summary>
        /// <returns> Name of the person who booked</returns>
        public string GetBookingName()
        {
            return nameBooking;
        }

        /// <summary>
        /// Returns the booked pax count
        /// </summary>
        /// <returns> Pax count of booking</returns>
        public int GetPaxCount()
        {
            return paxBooking;
        }

        /// <summary>
        /// Returns any aditional information about the booking
        /// </summary>
        /// <returns> Additional booking information</returns>
        public string GetAdditionalInformation()
        {
            return extraInformation;
        }
    }

    private void OnEnable()
    {
        bookings = new List<Bookings>();
        controller.GetBookingsForBookingsList();
        InvokeRepeating("DisplayNotification", 60 - DateTime.Now.Second, 60);
        CollectData.SetBookingsCount(bookings.Count);

    }

    /// <summary>
    /// Displays notifications for booking
    /// </summary>
    private void DisplayNotification()
    {
        foreach(Bookings booking in bookings)
        {
            if (booking.GetTimeBooked().AddMinutes(-15).Hour == DateTime.Now.Hour && booking.GetTimeBooked().AddMinutes(-15).Minute == DateTime.Now.Minute)
            {
                mainHostController.AddNotification($"\"{booking.GetBookingName()}\" booking for {booking.GetPaxCount()} pax is due in 15 minutes");
            }
        }
    }

    /// <summary>
    /// Here we receive one booking information
    /// </summary>
    /// <param name="time"> Time of booking hours and minutes</param>
    /// <param name="nameBooked"> Name of the person who booked</param>
    /// <param name="pax"> Pax of booking</param>
    /// <param name="info"> Any extra information if available</param>
    public void ReceiveInfo(DateTime time, string name, int pax, string info)
    {
        bookings.Add(new Bookings(time, name, pax, info));
    }

    /// <summary>
    /// Removes a single booking
    /// </summary>
    /// <param name="nameBooked"> Name of the person who booked</param>
    /// <param name="pax"> Pax of booking</param>
    /// <param name="info"> Any extra information if available</param>
    public void DeleteBooking(string nameBooked, int pax)
    {
        controller.DeleteBooking(nameBooked, pax, FindBooking(nameBooked, pax).GetAdditionalInformation());
        bookings.Remove(FindBooking(nameBooked, pax));
        RecreateBookings();
    }

    /// <summary>
    /// Finds a booking with the given name pax and info that matches
    /// </summary>
    /// <param name="nameBooked"> Name of the booking</param>
    /// <param name="pax"> Pax of the booking</param>
    /// <returns> Booking that matches all the criteria</returns>
    private Bookings FindBookingWithoutExtraInfo(string nameBooked, int pax)
    {
        foreach (Bookings booking in bookings)
        {
            if (booking.GetBookingName() == nameBooked && booking.GetPaxCount() == pax)
            {
                if(booking.GetAdditionalInformation() != null || booking.GetAdditionalInformation() != "")
                {
                    return booking;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Finds a booking with the given name pax and info that matches
    /// </summary>
    /// <param name="nameBooked"> Name of the booking</param>
    /// <param name="pax"> Pax of the booking</param>
    /// <param name="info"> Additional information of the booking</param>
    /// <returns> Booking that matches all the criteria</returns>
    private Bookings FindBooking(string nameBooked, int pax)
    {
        foreach (Bookings booking in bookings)
        {
            if (booking.GetBookingName() == nameBooked && booking.GetPaxCount() == pax)
            {
                return booking;
            }
        }
        return null;
    }

    /// <summary>
    /// Deletes old booking buttons and creates new ones
    /// </summary>
    public void RecreateBookings()
    {
        currentBookingNo = 0;
        DeleteOldBookingsButtons();
        SetContentSizeForButtons();
        CreateButtonsAndInfo();
    }

    /// <summary>
    /// If we have additional information on the button we create it with a button gameobject
    /// </summary>
    private void CreateButtonsAndInfo()
    {
        foreach (Bookings temp in bookings)
        {
            if (temp.GetAdditionalInformation() == "" || temp.GetAdditionalInformation() == null)
            {
                CreateBookingInfo(temp);
            }
            else
            {
                CreateBookingButton(temp);
            }
        }
    }

    /// <summary>
    /// Here we set the size of the content gameobject so we have enaugh space for all the bookings
    /// </summary>
    private void SetContentSizeForButtons()
    {
        bookingsList.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (bookings.Count) * 50);
    }

    /// <summary>
    /// Here we create the button with the booking information
    /// </summary>
    /// <param name="booking"> Original booking info</param>
    private void CreateBookingButton(Bookings booking)
    {
        GameObject tempGameobject = Instantiate(bookingButtonPrefab, bookingsList.transform);
        tempGameobject.transform.localPosition = new Vector3(0, 25 - 50 * (currentBookingNo + 1), 0);
        tempGameobject.GetComponent<RectTransform>().offsetMax = new Vector2(0, tempGameobject.GetComponent<RectTransform>().offsetMax.y);
        tempGameobject.GetComponent<RectTransform>().offsetMin = new Vector2(0, tempGameobject.GetComponent<RectTransform>().offsetMin.y);
        tempGameobject.name = booking.GetBookingName();
        tempGameobject.tag = "BookingHostingButton";
        tempGameobject.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = booking.GetTimeBooked().ToShortTimeString();
        tempGameobject.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = booking.GetBookingName();
        tempGameobject.transform.GetChild(1).transform.GetChild(2).GetComponent<Text>().text = booking.GetPaxCount().ToString();

        currentBookingNo++;
    }

    /// <summary>
    /// Here we create the text to show who is booking
    /// </summary>
    /// <param name="booking"> Original booking info</param>
    private void CreateBookingInfo(Bookings booking)
    {
        GameObject tempGameobject = Instantiate(bookingInfoPrefab, bookingsList.transform);
        tempGameobject.transform.localPosition = new Vector3(0, 25 - 50 * (currentBookingNo + 1), 0);
        tempGameobject.GetComponent<RectTransform>().offsetMax = new Vector2(0, tempGameobject.GetComponent<RectTransform>().offsetMax.y);
        tempGameobject.GetComponent<RectTransform>().offsetMin = new Vector2(0, tempGameobject.GetComponent<RectTransform>().offsetMin.y);
        tempGameobject.name = booking.GetBookingName();
        tempGameobject.tag = "BookingHostingButton";
        tempGameobject.transform.GetChild(1).GetComponent<Text>().text = booking.GetTimeBooked().ToShortTimeString();
        tempGameobject.transform.GetChild(2).GetComponent<Text>().text = booking.GetBookingName();
        tempGameobject.transform.GetChild(3).GetComponent<Text>().text = booking.GetPaxCount().ToString();

        currentBookingNo++;
    }

    /// <summary>
    /// Here we delete old booking buttons(found by tag) after something changes to reorganise them
    /// </summary>
    private void DeleteOldBookingsButtons()
    {
        GameObject[] tempList = GameObject.FindGameObjectsWithTag("BookingHostingButton");

        foreach(GameObject button in tempList)
        {
            Destroy(button);
        }
    }

    /// <summary>
    /// Here we enable the display extra information canvas
    /// </summary>
    /// <param name="nameBooked"> The name of the booking that we want extra info on</param>
    /// <param name="pax"> Pax of the booking</param>
    public void DisplayExtraInfo(string nameBooked, int pax)
    {
        if (!extraInfoText.activeSelf)
        {
            extraInfoText.SetActive(true);
        }
        extraInfoText.transform.GetChild(0).GetComponent<Text>().text = FindBookingWithoutExtraInfo(nameBooked, pax).GetAdditionalInformation();
    }
}
