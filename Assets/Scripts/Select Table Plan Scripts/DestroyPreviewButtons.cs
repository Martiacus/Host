using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPreviewButtons : MonoBehaviour
{
    /// <summary>
    /// Here we destroy all preview buttons that were created for the preview with their tag
    /// </summary>
    public void OnClick()
    {
        GameObject[] buttons;

        buttons = GameObject.FindGameObjectsWithTag("PreviewTablePlanButton");
        foreach (GameObject button in buttons)
        {
            Destroy(button.gameObject);
        }
    }

}
