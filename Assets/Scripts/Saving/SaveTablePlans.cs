using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SaveTablePlans
{
    public List<TablePlan> tablePlans;

    public List<string> selectedTablePlans;

    public List<TablePlan> onTablePlans;

    public SaveTablePlans(TablePlans tablePlansData)
    {
        tablePlans = tablePlansData.ReturnTablePlans();
        selectedTablePlans = tablePlansData.ReturnSelectedTablePlans();
        onTablePlans = tablePlansData.ReturnOnTablePlans();
    }
}
