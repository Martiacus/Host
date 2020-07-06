using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScriptsToRunAfterCheck : MonoBehaviour
{
    public GameObject collectButtonsInfo;                   // Here we store all of the buttons collected in the builder
    public GameObject getData;                              // Here we store all of our tables
    public GameObject changeCanvas;                         // Here we store our exit button that closes this canvas
    public GameObject nameChange;                           // Here have our plan name

    /// <summary>
    /// Here we run the scripts if the test was passed
    /// </summary>
    public void TestPassed()
    {
        collectButtonsInfo.GetComponent<TablePlanBuilderCollectButtonsInfo>().OnClick();
        getData.GetComponent<TablePlans>().GetData();
        changeCanvas.GetComponent<DestroyTablePlanBuilderButtons>().OnClick();
        nameChange.GetComponent<NullTablePlanNameInputField>().OnClick();
        SaveController.SaveUserTablePlans(getData.GetComponent<TablePlans>());

        changeCanvas.GetComponent<CanvasEnableDisable>().EnableCanvas();
    }
}
