using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableWantsToMoveButton : MonoBehaviour
{
    private TableWantsToMoveController movingController;
    public bool isSelected;

    private void Start()
    {
        movingController = GameObject.Find("WantsToMoveCanvas").GetComponent<TableWantsToMoveController>();
        isSelected = false;
    }

    /// <summary>
    /// Here we preform the needed action on click depending if it is already selected or not
    /// </summary>
    public void OnClick()
    {
        if(isSelected)
        {
            movingController.DeselectATablePlan(this.gameObject.name);
            isSelected = false;
            this.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            movingController.SelectATablePlan(this.gameObject.name);
            isSelected = true;
            this.gameObject.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
        }
    }
}
