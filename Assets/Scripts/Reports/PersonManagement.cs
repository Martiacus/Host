using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonManagement : MonoBehaviour
{
    private ReportsController reportsController;
    public Text nameField;
    public Text emailField;

    private void Start()
    {
        reportsController = this.gameObject.transform.parent.parent.parent.parent.parent.GetComponent<ReportsController>();
    }

    /// <summary>
    /// Sends information about deletion request
    /// </summary>
    public void Delete()
    {
        reportsController.DeletePerson(nameField.text, emailField.text);
    }

    /// <summary>
    /// Sends information about edit request
    /// </summary>
    public void Edit()
    {
        reportsController.EditPerson(nameField.text, emailField.text);
    }
}
