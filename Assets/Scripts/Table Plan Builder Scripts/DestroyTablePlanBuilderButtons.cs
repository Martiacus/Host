using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTablePlanBuilderButtons : MonoBehaviour
{
    /// <summary>
    /// Here we destroy all tabel plan buttons used in builder with their tag
    /// </summary>
    public void OnClick()
    {
        GameObject[] buttons;

        buttons = GameObject.FindGameObjectsWithTag("TablePlanBuilderButton");
        foreach (GameObject button in buttons)
        {
            Destroy(button.gameObject);
        }
    }
}
