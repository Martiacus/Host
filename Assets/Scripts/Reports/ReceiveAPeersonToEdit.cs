using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveAPeersonToEdit : MonoBehaviour
{
    public InputField inputName;
    public InputField inputEmail;

    public ErrorStateCanvas error;
    public ReportsController reportsController;

    public DisableCanvas disableCanvas;

    /// <summary>
    /// Here we receive the info of the person we will edit
    /// </summary>
    /// <param name="name"> Person name</param>
    /// <param name="email"> Person email</param>
    public void ReceiveInfo(string name, string email)
    {
        inputName.text = name;
        inputEmail.text = email;
    }

    /// <summary>
    /// Check for any errors in the information before saving
    /// </summary>
    public void CheckErrors()
    {
        if (inputName.text == null || inputName.text == "")    // Checks if the name field is empty
        {
            error.gameObject.SetActive(true);
            error.ErrorPleaseTypeAName();
        }
        else if (!IsValidEmailAddress(inputEmail.text))       // Checks if the email adress is at least somewhat in correct format
        {
            error.gameObject.SetActive(true);
            error.ErrorPleaseTypeAValidEmailAdress();
        }
        else
        {
            Save();
            disableCanvas.OnClick();
        }
    }

    /// <summary>
    /// Saves a person
    /// </summary>
    private void Save()
    {
        reportsController.AddPerson(inputName.text, inputEmail.text);
    }

    /// <summary>
    /// Checks string if it at least resembles and email(not perfect)
    /// </summary>
    /// <param name="s"> Email string</param>
    /// <returns> True if the email is valid, false if not</returns>
    private bool IsValidEmailAddress(string s)
    {
        if (string.IsNullOrEmpty(s))
            return false;
        else
        {
            var regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            return regex.IsMatch(s) && !s.EndsWith(".");
        }
    }
}
