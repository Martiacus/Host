using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveMailingList
{
    public List<string> namesAndEmails; // Names and emails separated by a new line


    public SaveMailingList(List<string> people)
    {
        namesAndEmails = people;
    }

}
