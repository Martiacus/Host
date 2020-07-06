using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveUserSettings
{
    public int tablePlanBuilderWidth;
    public int tablePlanBuilderHeight;
    public int timeForTableStayPerPerson;
    public int extraTableStayTime;

    public SaveUserSettings()
    {
        tablePlanBuilderWidth = SettingsController.GetTablePlanBuilderWidth();
        tablePlanBuilderHeight = SettingsController.GetTablePlanBuilderHeight();
        timeForTableStayPerPerson = SettingsController.GetProjectedTableStayPerPersonTime();
        extraTableStayTime = SettingsController.GetExtraTableStayTime();
    }
}
