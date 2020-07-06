using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSelectedTablePlans : MonoBehaviour
{
    /// <summary>
    /// Here we use a script meant for a prefab Delete button to find and execute the script to collect all table plans before deleting any and recreating the whole list
    /// </summary>
    public void OnClick()
    {
        this.gameObject.transform.parent.parent.parent.parent.gameObject.GetComponent<CollectOnTablePlans>().OnClick();
    }
}
