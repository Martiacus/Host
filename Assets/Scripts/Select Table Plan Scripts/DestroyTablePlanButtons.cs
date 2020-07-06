using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTablePlanButtons : MonoBehaviour
{
    /// <summary>
    /// Here we destroy all tabel plan buttons used in table plan selection with their tag
    /// </summary>
    public void OnClick()
    {
        GameObject[] buttons;

        buttons = GameObject.FindGameObjectsWithTag("TablePlanSelectionButton");
        foreach (GameObject button in buttons)
        {
            Destroy(button.gameObject);
        }
    }
}
