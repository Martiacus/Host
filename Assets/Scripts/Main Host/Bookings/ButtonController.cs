using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Text bookingName;
    public Text pax;

    public void ShowInfo()
    {
        FindParent().GetComponent<BookingsController>().DisplayExtraInfo(bookingName.text, int.Parse(pax.text));
    }

    private GameObject FindParent()
    {
        return this.gameObject.transform.parent.parent.parent.parent.parent.gameObject;
    }

    public void DeleteBooking()
    {
        FindParent().GetComponent<BookingsController>().DeleteBooking(bookingName.text, int.Parse(pax.text));
    }
}
