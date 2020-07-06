using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteTablePlan : MonoBehaviour
{
    /// <summary>
    /// Here we delete a specific table plan by name
    /// </summary>
    public void OnClick()
    {
        GameObject.Find("TablePlans").GetComponent<TablePlans>().DeleteTablePlan(this.gameObject.transform.parent.GetChild(1).GetComponent<Text>().text);
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
