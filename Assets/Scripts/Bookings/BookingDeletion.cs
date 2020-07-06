using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BookingDeletion : MonoBehaviour
{
    private Controller controller;

    public Text time;
    public Text bookingName;
    public Text pax;
    public Text additionalInfo;

    private void Start()
    {
        controller = GameObject.Find("Controller").GetComponent<Controller>();
    }

    public void Delete()
    {
        controller.DeleteBooking(bookingName.text, int.Parse(pax.text), additionalInfo.text);
    }
}