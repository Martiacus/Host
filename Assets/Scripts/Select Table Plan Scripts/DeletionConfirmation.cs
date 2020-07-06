using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletionConfirmation : MonoBehaviour
{
    GameObject tablePlan;

    /// <summary>
    /// Receive the gameobject that is asking if it is true or not
    /// </summary>
    /// <param name="gameObject"></param>
    public void RecieveData(GameObject gameObject)
    {
        tablePlan = gameObject;
    }

    /// <summary>
    /// If the button reply is "Yes" send the info back to the to the gameobject that requested the confirmation
    /// </summary>
    public void Yes()
    {
        tablePlan.GetComponent<DeleteTablePlansSelectionButtonManager>().RecieveAnswer(true);
        this.gameObject.SetActive(false);
    }


    /// <summary>
    /// If the button reply is "No" send the info back to the to the gameobject that requested the confirmation
    /// </summary>
    public void No()
    {
        tablePlan.GetComponent<DeleteTablePlansSelectionButtonManager>().RecieveAnswer(false);
        this.gameObject.SetActive(false);
    }
}
