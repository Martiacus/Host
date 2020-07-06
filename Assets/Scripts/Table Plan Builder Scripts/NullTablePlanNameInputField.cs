using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NullTablePlanNameInputField : MonoBehaviour
{
    /// <summary>
    /// Changes text to null after saving the name
    /// </summary>
    public void OnClick()
    {
        this.gameObject.GetComponent<InputField>().text = null;
    }
}
