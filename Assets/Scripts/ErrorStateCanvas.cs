using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorStateCanvas : MonoBehaviour
{
    public GameObject textGameobject;               // Here we find the gameobject that will show the error code

    // Here we store all of our Error texts and call out to them as needed

    public void ErrorNoDataToSend()
    {
        textGameobject.GetComponent<Text>().text = "Error, no data to send";
    }

    public void ReportsSentSuccesfully()
    {
        textGameobject.GetComponent<Text>().text = "Report was sent succesfully";
    }
    
    public void ErrorReportNotSent()
    {
        textGameobject.GetComponent<Text>().text = "Error, failed to send report, please check your internet connection";
    }

    public void ErrorSelectAtLeastOneRecipient()
    {
        textGameobject.GetComponent<Text>().text = "Error, please select at least one recipient";
    }

    public void ErrorPleaseTypeAValidEmailAdress()
    {
        textGameobject.GetComponent<Text>().text = "Error, please input in a valid email address";
    }

    public void ErrorPleaseTypeAName()
    {
        textGameobject.GetComponent<Text>().text = "Error, please input a name";
    }

    public void ErrorTablesAreTaken()
    {
        textGameobject.GetComponent<Text>().text = "Error, there is at least one table that has not left yet";
    }

    public void ErrorPleaseSelectBookingName()
    {
        textGameobject.GetComponent<Text>().text = "Error, please input booking name";
    }

    public void ErrorBookingsAreLeft()
    {
        textGameobject.GetComponent<Text>().text = "Error, There is at least one booking that is not confirmed yet.";
    }

    public void ErrorFinishPreviousAction()
    {
        textGameobject.GetComponent<Text>().text = "Error, please finish previous action before exiting.";
    }

    public void ErrorNameAlreadyExsits()
    {
        textGameobject.GetComponent<Text>().text = "Error, this name already exists, please choose a different one.";
    }

    public void ErrorThisNameIsInvalid()
    {
        textGameobject.GetComponent<Text>().text = "Error, this name is invalid, please choose a different one.";
    }

    public void ErrorSelectAtLeastOneTablePlan()
    {
        textGameobject.GetComponent<Text>().text = "Error, please select at least 1 table plan for hosting.";
    }

}
