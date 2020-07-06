using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTablePlanBuilder : MonoBehaviour
{
    public GameObject nameInputField;               // Here we get the name that the user created
    public GameObject errorCanvas;                  // Here we have the canvas that has the errors
    public GameObject saveGameobject;               // Here we have gameobject that has all of the scripts we need to run to save the table plan
    public void OnClick()
    {
        if (nameInputField.GetComponent<NameAlreadyExists>().CheckIfNameExsits())           // Here we check if name already exists (we can only have 1 copy of a single name)
        {
            errorCanvas.SetActive(true);
            errorCanvas.GetComponent<ErrorStateCanvas>().ErrorNameAlreadyExsits();
        }
        else if (!nameInputField.GetComponent<NameAlreadyExists>().CheckIfNameIsValid())    // Here we check if the name is valid, we compare it to a manually added names list through code
        {
            errorCanvas.SetActive(true);
            errorCanvas.GetComponent<ErrorStateCanvas>().ErrorThisNameIsInvalid();
        }
        else
        {   
            saveGameobject.GetComponent<SaveScriptsToRunAfterCheck>().TestPassed();         // Here send the info that the test was passed and that we can run the saving scripts
        }
    }
}
