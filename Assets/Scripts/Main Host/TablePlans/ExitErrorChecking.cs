using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitErrorChecking : MonoBehaviour
{
    public ErrorStateCanvas errorCanvas;
    public MainHostController controller;
    public GameObject confirmationCanvas;

    public void OnClick()
    {
        if(CheckErrors())
        {
            confirmationCanvas.SetActive(true);
        }
        else
        {
            EnableErrors();
        }
    }

    /// <summary>
    /// Enables error canvas and displays a message depending on the error
    /// </summary>
    private void EnableErrors()
    {
        errorCanvas.gameObject.SetActive(true);
        if (!CheckIsThereAnyTakenTables())
        {
            errorCanvas.ErrorTablesAreTaken();
        }
        else if (CheckIfAnyMovingIsTakingPlace())
        {
            errorCanvas.ErrorFinishPreviousAction();
        }
    }

    /// <summary>
    /// Here we preform the operations needed to exit form canvas
    /// </summary>
    public void Exit()
    {
        controller.SavePaxData();

        confirmationCanvas.SetActive(false);
        this.gameObject.GetComponent<DestroyAllTablePlanButtons>().OnClick();
        controller.ClearOldTables();
        this.gameObject.GetComponent<CanvasEnableDisable>().EnableCanvas();
    }

    /// <summary>
    /// Here we check all the errors
    /// </summary>
    /// <returns> True if there are no errors</returns>
    private bool CheckErrors()
    {
        if(CheckIsThereAnyTakenTables() && CheckIfAnyMovingIsTakingPlace())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Here we check if there are any free tables if there are no taken tables  true if there are table and false if no tables are taken
    /// </summary>
    /// <returns> False if there are taken tables</returns>
    bool CheckIsThereAnyTakenTables()
    {
        if(controller.ReturnTakenTableCount() == 0)
        {
            return true;
        }
        else
        {
            return false; 
        }
    }

    /// <summary>
    /// Retruns true if a table is moving and false if no table is moving
    /// </summary>
    /// <returns> False if table is moving</returns>
    bool CheckIfAnyMovingIsTakingPlace()
    {
        if(controller.IsTableMoving())
        {
            return false;
        }
        else
        {
            return true; 
        }
    }
}
