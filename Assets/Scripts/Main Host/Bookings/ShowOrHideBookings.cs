using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOrHideBookings : MonoBehaviour
{
    public GameObject bookings;
    public BookingsController controller;
    public GameObject extraText;

    public void Onclick()
    {
        if(bookings.activeSelf)
        {
            bookings.SetActive(false);
            extraText.SetActive(false);
        }
        else
        {
            bookings.SetActive(true);
            controller.RecreateBookings();
        }
    }
}
