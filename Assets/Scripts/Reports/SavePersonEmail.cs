using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SavePersonEmail : MonoBehaviour
{
    public InputField nameText;
    public InputField emailText;

    public ReportsController reportsController;

    public ErrorStateCanvas error;

    public DisableCanvas disableCanvas;

    public void CheckErrors()
    {
        if(nameText.text == null || nameText.text == "")    // Checks if the name field is empty
        {
            error.gameObject.SetActive(true);
            error.ErrorPleaseTypeAName();
        }
        else if(!IsValidEmailAddress(emailText.text))       // Checks if the email adress is at least somewhat in correct format
        {
            error.gameObject.SetActive(true);
            error.ErrorPleaseTypeAValidEmailAdress();
        }
        else
        {
            Save();
            NullFields();
            disableCanvas.OnClick();
        }
    }

    /// <summary>
    /// Makes input fields empty for future use
    /// </summary>
    public void NullFields()
    {
        nameText.text = null;
        emailText.text = null;
    }

    /// <summary>
    /// Saves a person
    /// </summary>
    private void Save()
    {
        reportsController.AddPerson(nameText.text, emailText.text);
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
