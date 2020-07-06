using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ReportsController : MonoBehaviour
{
    private List<Person> people;

    public GameObject personPrefab;
    public GameObject personManagerPrefab;

    public GameObject displayContent;
    public GameObject managerContent;

    public GameObject editPersonCanvas;

    public GameObject deletionConfirmation;

    private Person personToDelete;

    private int currentNo;

    private void Start()
    {
        LoadData();
        if(people == null)
        {
            people = new List<Person>();
        }
    }

    public void LoadData()
    {
        if(SaveController.LoadMailingList() != null)
        {
            people = new List<Person>();
            List<string> tempList = SaveController.LoadMailingList().namesAndEmails;
            if (tempList.Count > 0)
            {
                string[] namesAndEmails = new string[2];

                foreach (string tempString in tempList)
                {
                    namesAndEmails = tempString.Split('\n');
                    people.Add(new Person(namesAndEmails[0], namesAndEmails[1]));
                }
            }
        }
        CreateDisplayPeople();
    }

    

    public List<string> CollectEmails()
    {
        List<string> collectedEmails = new List<string>();

        GameObject[] tempList = GameObject.FindGameObjectsWithTag("DisplayPeople");

        foreach(GameObject person in tempList)
        {
            if(person.transform.GetChild(2).GetComponent<ButtonOnOff>().isButtonOn)
            {
                collectedEmails.Add(person.transform.GetChild(1).GetComponent<Text>().text);
            }
        }
        return collectedEmails;
    }

    private class Person
    {
        string name;
        string email;

        public Person(string sentName, string sentEmail)
        {
            name = sentName;
            email = sentEmail;
        }

        public string GetName()
        {
            return name;
        }

        public string GetEmail()
        {
            return email;
        }
    }

    public void AddPerson(string personName, string personEmail)
    {
        if(people == null || people.Count > 0)
        {
            people = new List<Person>();
        }
        people.Add(new Person(personName, personEmail));
        CreateDisplayPeople();
        CreateDisplayPeopleForManagement();
        Save();
    }

    public void EditPerson(string senderName, string senderEmail)
    {
        editPersonCanvas.SetActive(true);
        editPersonCanvas.GetComponent<ReceiveAPeersonToEdit>().ReceiveInfo(senderName, senderEmail);
        DeletePerson(senderName, senderEmail);
    }

    public void DeletePerson(string name, string email)
    {
        deletionConfirmation.SetActive(true);
        personToDelete = new Person(name, email);
    }

    public void ConfirmDelete()
    {
        deletionConfirmation.SetActive(false);
        foreach (Person person in people)
        {
            if (person.GetName() == personToDelete.GetName() && person.GetEmail() == personToDelete.GetEmail())
            {
                people.Remove(person);
                CreateDisplayPeople();
                CreateDisplayPeopleForManagement();
                Save();
                personToDelete = null;
                return;
            }
        }
    }

    private void Save()
    {
        SaveController.SaveMailingList(FormatPeopleClassToString());
    }

    private List<string> FormatPeopleClassToString()
    {
        List<string> tempString = new List<string>();
        foreach(Person person in people)
        {
            tempString.Add($"{person.GetName()}\n{person.GetEmail()}");
        }

        return tempString;
    }

    private void OnEnable()
    {
        if (people != null)
        {
            CreateDisplayPeople();
        }
    }


    /// <summary>
    /// Creates a display for people
    /// </summary>
    public void CreateDisplayPeople()
    {
        if(people.Count > 0 || people != null)
        {
            DeleteButtons(true);
            displayContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (people.Count) * 100);
            currentNo = 0;
            foreach (Person person in people)
            {
                CreatePeopleForDisplay(person);
            }
        }
    }

    /// <summary>
    /// Creates a display for people
    /// </summary>
    public void CreateDisplayPeopleForManagement()
    {
        if (people.Count > 0 || people != null)
        {
            DeleteButtons(false);
            managerContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (people.Count) * 100);
            currentNo = 0;
            foreach (Person person in people)
            {
                CreatePeopleForManagement(person);
            }
        }
    }

    /// <summary>
    /// Creates a single button for displaying in main reports menu
    /// </summary>
    /// <param name="person"> Persons information</param>
    private void CreatePeopleForDisplay(Person person)
    {
        GameObject tempGameobject = Instantiate(personPrefab, displayContent.transform);
        tempGameobject.transform.localPosition = new Vector3(0, 50 - 100 * (currentNo + 1), 0);
        tempGameobject.GetComponent<RectTransform>().offsetMax = new Vector2(0, tempGameobject.GetComponent<RectTransform>().offsetMax.y);
        tempGameobject.GetComponent<RectTransform>().offsetMin = new Vector2(0, tempGameobject.GetComponent<RectTransform>().offsetMin.y);
        tempGameobject.name = person.GetName();
        tempGameobject.tag = "DisplayPeople";
        tempGameobject.transform.GetChild(0).gameObject.GetComponent<Text>().text = person.GetName();
        tempGameobject.transform.GetChild(1).gameObject.GetComponent<Text>().text = person.GetEmail();
        currentNo++;
    }

    /// <summary>
    /// Creates buttons for management (deletion of people and changing their info)
    /// </summary>
    /// <param name="person"></param>
    private void CreatePeopleForManagement(Person person)
    {
        GameObject tempGameobject = Instantiate(personManagerPrefab, managerContent.transform);
        tempGameobject.transform.localPosition = new Vector3(0, 50 - 100 * (currentNo + 1), 0);
        tempGameobject.GetComponent<RectTransform>().offsetMax = new Vector2(0, tempGameobject.GetComponent<RectTransform>().offsetMax.y);
        tempGameobject.GetComponent<RectTransform>().offsetMin = new Vector2(0, tempGameobject.GetComponent<RectTransform>().offsetMin.y);
        tempGameobject.name = person.GetName();
        tempGameobject.tag = "DisplayPeopleForManager";
        tempGameobject.transform.GetChild(0).gameObject.GetComponent<Text>().text = person.GetName();
        tempGameobject.transform.GetChild(1).gameObject.GetComponent<Text>().text = person.GetEmail();

        currentNo++;
    }

    /// <summary>
    /// Deletes buttons, true for people, false for management
    /// </summary>
    /// <param name="forDisplay"> if true deletes display people, if false deletes management people</param>
    public void DeleteButtons(bool forDisplay)
    {
        string tag;
        if (forDisplay)
        {
            tag = "DisplayPeople";
        }
        else
        {
            tag = "DisplayPeopleForManager";
        }

        GameObject[] buttons = GameObject.FindGameObjectsWithTag(tag);

        foreach(GameObject button in buttons)
        {
            Destroy(button);
        }
    }

    /// <summary>
    /// Deletes all buttons for display and management
    /// </summary>
    public void DeleteButtons()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("DisplayPeople");

        foreach (GameObject button in buttons)
        {
            Destroy(button);
        }

        buttons = GameObject.FindGameObjectsWithTag("DisplayPeopleForManager");

        foreach (GameObject button in buttons)
        {
            Destroy(button);
        }

    }

}
