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

    public void Delete()
    {
        reportsController.DeletePerson(nameField.text, emailField.text);
    }

    public void Edit()
    {
        reportsController.EditPerson(nameField.text, emailField.text);
    }
}
