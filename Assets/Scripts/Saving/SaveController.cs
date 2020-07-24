using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveController
{
    /// <summary>
    /// Saves user settings
    /// </summary>
    public static void SaveSettings()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "settings.host");
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveUserSettings settingsSaved = new SaveUserSettings();

        formatter.Serialize(stream, settingsSaved);
        stream.Close();
    }

    /// <summary>
    /// Saves user created table plans
    /// </summary>
    /// <param name="plansGameobject"> table plans gameobhect that we want to save</param>
    public static void SaveUserTablePlans(TablePlans plansGameobject)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "tablePlans.host");
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveTablePlans plansSaved = new SaveTablePlans(plansGameobject);

        formatter.Serialize(stream, plansSaved);
        stream.Close();
    }

    /// <summary>
    /// Saves the names and emails of the user from mailing list
    /// </summary>
    /// <param name="people"></param>
    public static void SaveMailingList(List<string> people)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "mailingList.host");
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveMailingList mailsSaved = new SaveMailingList(people);

        formatter.Serialize(stream, mailsSaved);
        stream.Close();

    }

    /// <summary>
    /// Loads settings from file
    /// </summary>
    /// <returns></returns>
    public static SaveUserSettings LoadSettings()
    {
        string path = Path.Combine(Application.persistentDataPath, "settings.host");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveUserSettings settingsLoaded = formatter.Deserialize(stream) as SaveUserSettings;
            stream.Close();
            
            return settingsLoaded;
        }
        else
        {
            Debug.LogError($"\"settings.host\" save file not found in {path}");
            return null;
        }
    }

    /// <summary>
    /// Loads table plans from file
    /// </summary>
    /// <returns></returns>
    public static SaveTablePlans LoadTablePlans()
    {
        string path = Path.Combine(Application.persistentDataPath, "tablePlans.host");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveTablePlans plansSaved = formatter.Deserialize(stream) as SaveTablePlans;
            stream.Close();

            return plansSaved;
        }
        else
        {
            Debug.LogError($"\"tablePlans.host\" save file not found in {path}");
            return null;
        }
    }

    /// <summary>
    /// Loads mailing list from file
    /// </summary>
    /// <returns></returns>
    public static SaveMailingList LoadMailingList()
    {
        string path = Path.Combine(Application.persistentDataPath, "mailingList.host");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveMailingList mailsSaved = formatter.Deserialize(stream) as SaveMailingList;
            stream.Close();

            return mailsSaved;
        }
        else
        {
            Debug.LogError($"\"mailingList.host\" save file not found in {path}");
            return null;
        }
    }


}
