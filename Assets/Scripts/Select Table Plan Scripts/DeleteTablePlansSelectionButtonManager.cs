using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTablePlansSelectionButtonManager : MonoBehaviour
{
    /// <summary>
    /// Here we turn on the delete confirmation canvas and send this gameobject to that canvas to receive confirmation
    /// </summary>
    public void OnClick()
    {
        this.gameObject.transform.parent.parent.parent.parent.parent.GetChild(4).gameObject.SetActive(true);
        this.gameObject.transform.parent.parent.parent.parent.parent.GetChild(4).gameObject.GetComponent<DeletionConfirmation>().RecieveData(this.gameObject);
    }

    /// <summary>
    /// Receive answer for deleting table plan
    /// </summary>
    /// <param name="yesOrNo"> Yes is true no is false</param>
    public void RecieveAnswer(bool yesOrNo)
    {
        if(yesOrNo)
        {
            this.gameObject.GetComponent<DeleteTablePlan>().OnClick();
            this.gameObject.GetComponent<SaveSelectedTablePlans>().OnClick();
            this.gameObject.GetComponent<DestroyTablePlanButtons>().OnClick();
            this.gameObject.GetComponent<RecreateTablePlanButtons>().OnClick();
        }
    }
}
