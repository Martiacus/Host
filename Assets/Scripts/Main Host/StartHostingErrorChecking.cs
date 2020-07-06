using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHostingErrorChecking : MonoBehaviour
{
    // Theese gameobjects are only for activating their scripts
    public GameObject buttonStartHosting;
    public GameObject controller;
    public GameObject mainCanvas;
    public GameObject TablePlansCanvas;

    public GameObject errorStateCanvas;     // This is our error state canvas that we send the error to to display to the user
    public GameObject tablePlansData;       // This is our TablePlans gameobject that stores all tables and plans data

    public GameObject deleteDataCanvas;

    /// <summary>
    /// Here we start testing the table plans count
    /// </summary>
    public void OnClick()
    {
        Test();
    }

    /// <summary>
    /// Here we test if we have enaugh table plans active ( at least 1 ) to launch the hosting application
    /// </summary>
    private void Test()
    {
        if(!CollectData.IsDataEmpty())
        {
            deleteDataCanvas.SetActive(true);
        }
        else if (tablePlansData.GetComponent<TablePlans>().selectedTablePlans.Count > 0)
        {
            TestPassed();
        }
        else
        {
            TestFailed();
        }
    }

    /// <summary>
    /// Deletes collected data and sets the values to null
    /// </summary>
    public void DeleteCollectedData()
    {
        CollectData.ResetData();
    }

    /// <summary>
    /// If test failed we send the info to error canvas to display error message
    /// </summary>
    private void TestFailed()
    {
        errorStateCanvas.SetActive(true);
        errorStateCanvas.GetComponent<ErrorStateCanvas>().ErrorSelectAtLeastOneTablePlan();
    }

    /// <summary>
    /// If the test failed we just run the scripts as normal
    /// </summary>
    private void TestPassed()
    {
        buttonStartHosting.GetComponent<CanvasEnableDisable>().EnableCanvas();
        controller.GetComponent<Controller>().OnHostStart();
        mainCanvas.GetComponent<CreateTablePlanButtons>().CreateButtons();
        TablePlansCanvas.GetComponent<CreateTablePlansCanvasesByName>().OnClick();
        if(deleteDataCanvas.activeSelf)
        {
            deleteDataCanvas.SetActive(false);
        }
        CollectData.ResetData();
    }
}
