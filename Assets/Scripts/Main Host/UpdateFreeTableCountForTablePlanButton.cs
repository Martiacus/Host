using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateFreeTableCountForTablePlanButton : MonoBehaviour
{
    private GameObject[] tables;

    /// <summary>
    /// Here we show the free table count
    /// </summary>
    public void Update()
    {
        this.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Free Tables: " + Count();
    }

    /// <summary>
    /// Counts all free tables in the table plan button
    /// </summary>
    /// <returns> Total number of free tables in that table plan </returns>
    public int Count()
    {
        tables = GameObject.FindGameObjectsWithTag("TablePlanButtonTableImage");

        int count = 0;

        foreach(GameObject table in tables)
        {
            if(table.GetComponent<IsTableImageFree>().IsTableFree() && table.transform.parent.parent == this.gameObject.transform)
            {
                count++;
            }
        }
        return count;
    }
}
