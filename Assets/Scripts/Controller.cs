using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    List<Plan> plans;
    GameObject data;
    List<Bookings> bookings;

    public GameObject exitButtonForMainCanvas;
    public TableWantsToMoveController tableWantsToMoveController;
    public CreateBookingsButtons bookingsCanvas;
    public BookingsController mainHostBookingsController;

    class Bookings
    {
        DateTime timeBooked;
        string nameBooking;
        int paxBooking;
        string extraInformation;

        /// <summary>
        /// Sets the data for a single booking
        /// </summary>
        /// <param name="time"> Time of booking hours and minutes</param>
        /// <param name="nameBooked"> Name of the person who booked</param>
        /// <param name="pax"> Pax of booking</param>
        /// <param name="info"> Any extra information if available</param>
        public Bookings(DateTime time, string nameBooked, int pax, string info)
        {
            timeBooked = time;
            nameBooking = nameBooked;
            paxBooking = pax;
            extraInformation = info;
        }

        /// <summary>
        /// Retruns the time the booking was placed
        /// </summary>
        /// <returns> Booking time</returns>
        public DateTime GetTimeBooked()
        {
            return timeBooked;
        }

        /// <summary>
        /// Return the name the booking is under
        /// </summary>
        /// <returns> Name of the person who booked</returns>
        public string GetBookingName()
        {
            return nameBooking;
        }

        /// <summary>
        /// Returns the booked pax count
        /// </summary>
        /// <returns> Pax count of booking</returns>
        public int GetPaxCount()
        {
            return paxBooking;
        }

        /// <summary>
        /// Returns any aditional information about the booking
        /// </summary>
        /// <returns> Additional booking information</returns>
        public string GetAdditionalInformation()
        {
            return extraInformation;
        }
    }

    class Plan
    {
        string name;                    // Our plan name
        GameObject planGameobject;      // Our plan canvas gamobject (enlarged version)
        GameObject buttonGameobject;    // Our button gameobject (mini version before going to an enlarged version)
        public List<Table> tables;      // All the tables in this plan

        public Plan(string name1)
        {
            name = name1;
            tables = new List<Table>();
        }

        public GameObject GetPlanGameObject()
        {
            return planGameobject;
        }

        public GameObject GetButtonGameObject()
        {
            return buttonGameobject;
        }

        public void SetButtonGameobject(GameObject gameObject)
        {
            buttonGameobject = gameObject;
        }

        public void SetPlanGameobject(GameObject gameObject)
        {
            planGameobject = gameObject;
        }

        public string GetPlanName()
        {
            return name;
        }

        public Table GetTableByName(string name)
        {
            foreach(Table table in tables)
            {
                if(table.GetTableName() == name)
                {
                    return table;
                }
            }
            return null;
        }
    }

    class Table
    {
        string name;                // Table name, generally Table: X-Y (x and y are coordinates index)
        GameObject image;           // Image gamobject (the mini version) of the plan
        GameObject button;          // Table button gameobject that we can interact with (enalrged version of the plan)
        bool isTableTaken;          // Here we set if the table is taken or not
        int pax;                    // How many people are seated on this table
        DateTime projectedLeavingTime;   // The time the table is guessed to leave at
        DateTime timeSeated;        // The time the table was seated

        /// <summary>
        /// Creating a new table
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="isTableTaken1"></param>
        public Table(string name1, bool isTableTaken1)
        {
            name = name1;
            isTableTaken = isTableTaken1;
            projectedLeavingTime = DateTime.Now; // The time we are assuming the table will be free
            projectedLeavingTime.AddDays(1);     // Here we add an extra day just to not mess up the data
        }

        /// <summary>
        /// Here we switch table colors according if it was in use or not and changing its status(Green and red)
        /// </summary>
        public void SwitchIsTableInUse()
        {
            if(isTableTaken)
            {
                image.GetComponent<Image>().color = new Color(0, 255, 0);
                button.GetComponent<Image>().color = new Color(0, 255, 0);
                isTableTaken = false;
            }
            else
            {
                image.GetComponent<Image>().color = new Color(255, 0, 0);
                button.GetComponent<Image>().color = new Color(255, 0, 0);
                isTableTaken = true;
            }
        }

        /// <summary>
        /// Returns the time the table is projected to be leaving
        /// </summary>
        /// <returns> Projected table leaving time</returns>
        public DateTime GetProjectedTableLeavingTime()
        {
            return projectedLeavingTime;
        }

        /// <summary>
        /// Returns the ture of false statement if the table is taken
        /// </summary>
        /// <returns> True or false if the table is taken</returns>
        public bool IsTableTaken()
        {
            return isTableTaken;
        }

        /// <summary>
        /// Sets our guest pax on this table to a new 1 ( the pax cannot be lower than 1 because then you have a taken table with no people sitting on it)
        /// </summary>
        /// <param name="count"> The new guest pax</param>
        public void SetGuesPax(int count)
        {
            if(count > 0)
            {
                pax = count;
            }
            // Add error statae that pax has to be above 0 if its 0 ask to close the table
        }

        /// <summary>
        /// Returns current table name
        /// </summary>
        /// <returns> Table name</returns>
        public string GetTableName()
        {
            return name;
        }

        /// <summary>
        /// Here we set the image gameobject of the table
        /// </summary>
        /// <param name="image1"> the image gameobject</param>
        public void SetTableImage(GameObject image1)
        {
            image = image1;
        }

        /// <summary>
        /// Here we set the button gameobject of the table
        /// </summary>
        /// <param name="button1"> The button gameobject</param>
        public void SetTableButton(GameObject button1)
        {
            button = button1;
        }

        /// <summary>
        /// Here we return the image gameobject of the table
        /// </summary>
        /// <returns></returns>
        public GameObject GetTableImageGameobject()
        {
            return image;
        }
    }

    void Start()
    {
        data = GameObject.Find("TablePlans");           // Here we can find all table plans and table buttons
        LoadData();
        bookings = new List<Bookings>();
        CollectData.StartCollecting();
    }

    /// <summary>
    /// Here we reference the scripts that load their specific data from different files
    /// </summary>
    private void LoadData()
    {
        if(SaveController.LoadSettings() != null)
        {
            LoadSettings(SaveController.LoadSettings());
        }
        else
        {
            SettingsController.SetDefaultValues();
        }
        if(SaveController.LoadTablePlans() != null)
        {
            LoadTablePlans(SaveController.LoadTablePlans());
        }
        else
        {
            SaveController.SaveUserTablePlans(data.GetComponent<TablePlans>());

        }
    }

    /// <summary>
    /// Here we load settings configutration
    /// </summary>
    /// <param name="settings"> Our saved settings</param>
    private void LoadSettings(SaveUserSettings settings)
    {
        SettingsController.SetTablePlanBuilderWidthAndHeight(settings.tablePlanBuilderWidth, settings.tablePlanBuilderHeight);
        SettingsController.SetTableStayExtensionTime(settings.extraTableStayTime);
        SettingsController.SetTableStayPerPersonTime(settings.timeForTableStayPerPerson);
    }

    /// <summary>
    /// Here we load all table plans data
    /// </summary>
    /// <param name="savedTablePlans"> Our saved table plans</param>
    private void LoadTablePlans(SaveTablePlans savedTablePlans)
    {
        data.GetComponent<TablePlans>().SetLoadedData(savedTablePlans.tablePlans, savedTablePlans.selectedTablePlans, savedTablePlans.onTablePlans);
    }

    /// <summary>
    /// Returns bookings count
    /// </summary>
    /// <returns> Bookings count</returns>
    public int GetBookingsCount()
    {
        if(bookings.Count > 0 && bookings != null)
        {
            return bookings.Count;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Adds a new booking
    /// </summary>
    /// <param name="time"> Time of booking hours and minutes</param>
    /// <param name="nameBooked"> Name of the person who booked</param>
    /// <param name="pax"> Pax of booking</param>
    /// <param name="info"> Any extra information if available</param>
    public void AddBooking(DateTime time, string nameBooked, int pax, string info)
    {
        bookings.Add(new Bookings(time, nameBooked, pax, info));

        bookingsCanvas.ClearAllCurrentBookingsButtons();
        GetBookingsForBookingsCanvas();
    }

    /// <summary>
    /// Removes a single booking
    /// </summary>
    /// <param name="nameBooked"> Name of the person who booked</param>
    /// <param name="pax"> Pax of booking</param>
    /// <param name="info"> Any extra information if available</param>
    public void DeleteBooking(string nameBooked, int pax, string info)
    {
        bookings.Remove(FindBooking(nameBooked, pax, info));

        bookingsCanvas.ClearAllCurrentBookingsButtons();
        GetBookingsForBookingsCanvas();
    }

    /// <summary>
    /// Finds a booking with the given name pax and info that matches
    /// </summary>
    /// <param name="nameBooked"> Name of the booking</param>
    /// <param name="pax"> Pax of the booking</param>
    /// <param name="info"> Additional information of the booking</param>
    /// <returns> Booking that matches all the criteria</returns>
    private Bookings FindBooking(string nameBooked, int pax, string info)
    {
        foreach (Bookings booking in bookings)
        {
            if (booking.GetBookingName() == nameBooked && booking.GetPaxCount() == pax && booking.GetAdditionalInformation() == info)
            {
                return booking;
            }
        }
        return null;
    }

    /// <summary>
    /// Sends the list of bookings back to the bookings canvas to display all bookings
    /// </summary>
    /// <param name="sender"> Bookings canvas gameobject</param>
    public void GetBookingsForBookingsCanvas()
    {
        if (bookings != null)
        {
            bookingsCanvas.ReceiveBookingsCount(bookings.Count);

            foreach (Bookings booking in bookings)
            {
                bookingsCanvas.ReceiveABooking(booking.GetTimeBooked(), booking.GetBookingName(), booking.GetPaxCount(), booking.GetAdditionalInformation());
            }
        }
    }

    /// <summary>
    /// Sends the list of bookings to main host to display to the user
    /// </summary>
    /// <param name="sender"> Bookings controller</param>
    public void GetBookingsForBookingsList()
    {
        if (bookings != null)
        {
            foreach (Bookings booking in bookings)
            {
                mainHostBookingsController.ReceiveInfo(booking.GetTimeBooked(), booking.GetBookingName(), booking.GetPaxCount(), booking.GetAdditionalInformation());
            }
        }
    }

    /// <summary>
    /// Here we send the data of all selected table plans list to the controller that manages table moving to a different section
    /// </summary>
    public void GetSeletedTablePlans()
    {
        foreach(Plan plan in plans)
        {
            tableWantsToMoveController.ReceiveData(plan.GetPlanName(), plan.GetPlanGameObject());
        }
    }

    /// <summary>
    /// Here we set the main canvas to active
    /// </summary>
    /// <param name="name"> The name of the plan so we can find the main canvas</param>
    public void SetMainCanvasToActive(string name)
    {
        GetPlanByName(name).GetButtonGameObject().transform.parent.gameObject.SetActive(true);
    }

    /// <summary>
    /// Here we find and return all free table count
    /// </summary>
    /// <returns> Free table count</returns>
    public int ReturnAllFreeTableCount()
    {
        int tempint = 0;
        foreach(Plan plan in plans)
        {
            foreach(Table table in plan.tables)
            {
                if(!table.IsTableTaken())
                {
                    tempint++;
                }
            }
        }
        return tempint;
    }

    /// <summary>
    /// Here we set the plan canvas to acctive
    /// </summary>
    /// <param name="name"> The name of the plan</param>
    public void SetPlanToActive(string name)
    {
        GetPlanByName(name).GetPlanGameObject().SetActive(true);
    }

    /// <summary>
    /// Returns all active plans in current scene
    /// </summary>
    /// <returns> All active plans</returns>
    public GameObject[] ReturnAllPlans()
    {
        GameObject[] planGameobjects = new GameObject[plans.Count];
        int tempInt = 0;
        foreach(Plan plan in plans)
        {
            planGameobjects[tempInt] = plan.GetPlanGameObject();
            tempInt++;
        }
        return planGameobjects;
    }

    /// <summary>
    /// Here we cycle between green and red colors for table availability
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="planName"></param>
    public void SwithcTableInUse(string tableName, string planName)
    {
        GetPlanByName(planName).GetTableByName(tableName).SwitchIsTableInUse();
    }

    /// <summary>
    /// Here we assign a gameobject to our specific plan to be able to access it anytime
    /// </summary>
    /// <param name="name"></param>
    /// <param name="gameObject"></param>
    public void AddGameobjectToPlan(string name, GameObject gameObject)
    {
        GetPlanByName(name).SetPlanGameobject(gameObject);
    }

    /// <summary>
    /// Here we assign a button gameobject to our specific plan to be able to access it anytime
    /// </summary>
    /// <param name="name"> name of the plan we are assigning the button to</param>
    /// <param name="gameObject"> the button</param>
    public void AddGameobjectToPlanButton(string name, GameObject gameObject)
    {
        GetPlanByName(name).SetButtonGameobject(gameObject);
    }

    /// <summary>
    /// Here we retturn a specific plan by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns> Returns a plan with the given parameters orr null if not found</returns>
    private Plan GetPlanByName(string name)
    {
        foreach(Plan plan in plans)
        {
            if(plan.GetPlanName() == name)
            {
                return plan;
            }
        }
        return null;
    }

    /// <summary>
    /// Here we return image gameobject for a specific table in a psecific table plan
    /// </summary>
    /// <param name="planName"> The plan name we are looking in</param>
    /// <param name="tableName"> The name of the table inside that plan</param>
    /// <returns> Table image that is connected to the table button</returns>
    public GameObject GetTableImageByTableGameobject(string planName, string tableName)
    {
        return FindTableByNameInPlan(tableName ,FindPlanByName(planName)).GetTableImageGameobject();
    }

    /// <summary>
    /// Here on launching the hosting function we clear all the previous data we might have had and collect new data dpeneding on what was changed
    /// </summary>
    public void OnHostStart()
    {
        if (plans != null)
        {
            plans.Clear();
        }
        CollectTablePlanNames();
    }

    /// <summary>
    /// Here we find a specific table (by name) in a given plan
    /// </summary>
    /// <param name="name"> Name of the table we are looking for</param>
    /// <param name="plan"> The table plan we are looking in for that table</param>
    /// <returns> The table we are looking for or null if it was not found</returns>
    private Table FindTableByNameInPlan(string name, Plan plan)
    {
        if (plan == null)
        {
            Debug.Log("plan is null");
        }
        if (plan.tables != null)
        {
            foreach (Table table in plan.tables)
            {
                if (table.GetTableName() == name)
                {
                    return table;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Here we find and return a plan with a given name (all names are unique)
    /// </summary>
    /// <param name="name"> Name we are looking up</param>
    /// <returns> The plan we are looking for or null</returns>
    private Plan FindPlanByName(string name)
    {
        foreach (Plan plan in plans)
        {
            if(plan.GetPlanName() == name)
            {
                return plan;
            }
        }
        return null;
    }

    /// <summary>
    /// Here we add a button gameobject of a specific table, if the table is not found we create it
    /// </summary>
    /// <param name="planName"> The name of the plan we are going to add the button to</param>
    /// <param name="tableName"> The name of the table we are looking for or creating</param>
    /// <param name="button"> The gameobject we are attaching to that table</param>
    public void AddTableButton(string planName, string tableName, GameObject button)
    {
        Plan tempPlan = FindPlanByName(planName);                                   // Here we find the plan we want to access
        if(FindTableByNameInPlan(tableName, tempPlan) == null)                      // Here we look for and check if the table already exists (is not null) if it doesnt exist we create a new 1
        {
            Table tempTable = new Table(tableName, false);                          // Creating a new table
            tempPlan.tables.Add(tempTable);                                         // Adding the new table to the table plan
            tempTable.SetTableButton(button);                                       // Setting the button gameobject
        }
        else
        {
            FindTableByNameInPlan(tableName, tempPlan).SetTableButton(button);      // If we do not need to create the table plan we just add the gameobject to the table
        }
    }

    /// <summary>
    /// Here we add a button gameobject of a specific table, if the table is not found we create it
    /// </summary>
    /// <param name="planName"> The name of the plan we are going to add the button to</param>
    /// <param name="tableName"> The name of the table we are looking for or creating</param>
    /// <param name="image"> The gameobject we are attaching to that table</param>
    public void AddTableImage(string planName, string tableName, GameObject image)
    {
        Plan tempPlan = FindPlanByName(planName);                                   // Here we find the plan we want to access
        if (FindTableByNameInPlan(tableName, tempPlan) == null)                     // Here we look for and check if the table already exists (is not null) if it doesnt exist we create a new 1
        {
            Table tempTable = new Table(tableName, false);                          // Creating a new table
            tempPlan.tables.Add(tempTable);                                         // Adding the new table to the table plan
            tempTable.SetTableImage(image);                                        // Setting the button gameobject
        }
        else
        {
            FindTableByNameInPlan(tableName, tempPlan).SetTableImage(image);       // If we do not need to create the table plan we just add the gameobject to the table
        }
    }

    /// <summary>
    /// Here we collect all the selecteed table plan names and create plans with those names
    /// </summary>
    private void CollectTablePlanNames()
    {
        plans = new List<Plan>();
        foreach (string planName in data.GetComponent<TablePlans>().ReturnSelectedTablePlans() )
        {
            plans.Add(new Plan(planName));
        }
    }
}
