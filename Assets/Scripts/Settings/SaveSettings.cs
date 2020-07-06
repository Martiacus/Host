using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSettings : MonoBehaviour
{
    public Text tablePlanBuilderXCount;
    public Text tablePlanBuilderYCount;
    public Text projectedTableStayTimeInMinutes;
    public Text extraTableStayTime;

    private void Start()
    {
        LoadData();
    }

    /// <summary>
    /// Loads data from settings
    /// </summary>
    private void LoadData()
    {
        tablePlanBuilderXCount.text = SettingsController.GetTablePlanBuilderHeight().ToString();
        tablePlanBuilderYCount.text = SettingsController.GetTablePlanBuilderWidth().ToString();
        projectedTableStayTimeInMinutes.text = SettingsController.GetProjectedTableStayPerPersonTime().ToString();
        extraTableStayTime.text = SettingsController.GetExtraTableStayTime().ToString();
    }

    /// <summary>
    /// Saves the data
    /// </summary>
    public void OnClick()
    {
        SettingsController.SetTablePlanBuilderWidthAndHeight(int.Parse(tablePlanBuilderYCount.text), int.Parse(tablePlanBuilderXCount.text));
        SettingsController.SetTableStayPerPersonTime(int.Parse(projectedTableStayTimeInMinutes.text));
        SettingsController.SetTableStayExtensionTime(int.Parse(extraTableStayTime.text));
        SaveController.SaveSettings();
    }

    /// <summary>
    /// Restores the settings to default settings
    /// </summary>
    public void RestoreDefaults()
    {
        tablePlanBuilderXCount.text = "10";
        tablePlanBuilderYCount.text = "15";
        projectedTableStayTimeInMinutes.text = "15";
        extraTableStayTime.text = "0";

        OnClick();
    }
}
