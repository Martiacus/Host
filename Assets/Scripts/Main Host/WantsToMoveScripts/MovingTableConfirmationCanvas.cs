using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingTableConfirmationCanvas : MonoBehaviour
{
    public Text message;
    public MainHostController mainHostController;
    private bool isTableFree;

    /// <summary>
    /// Here we receive the data if the table that moving is taking place is free or not ad display appropriate text
    /// </summary>
    public void TableIsFree()
    {
        message.text = "Do you want to move to this table?";
        isTableFree = true;
    }

    /// <summary>
    /// Here we receive the data if the table that moving is taking place is free or not ad display appropriate text
    /// </summary>
    public void TableIsTaken()
    {
        message.text = "Do you want to merge theese tables?";
        isTableFree = false;
    }

    /// <summary>
    /// Here we send a positive reply to the merge or transfer of table
    /// </summary>
    public void Confirm()
    {
        if(isTableFree)
        {
            mainHostController.MoveToFreeTable();
        }
        else
        {
            mainHostController.MergeTwoTables();
        }
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Here we send a negative reply to the merge or transfer of table
    /// </summary>
    public void Deny()
    {
        this.gameObject.SetActive(false);
    }
}
