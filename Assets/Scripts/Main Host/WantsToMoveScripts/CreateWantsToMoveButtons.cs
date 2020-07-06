using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateWantsToMoveButtons : MonoBehaviour
{
    public GameObject buttonPrefab;
    public TableWantsToMoveController movingController;
    private List<GameObject> tablePlans;

    public void OnEnable()
    {
        GetTablePlanGameobjects();
        CreateButtons();
    }

    /// <summary>
    /// Get the table plan gameobjects for referencing the plans
    /// </summary>
    private void GetTablePlanGameobjects()
    {
        tablePlans = movingController.GetTablePlans();
    }

    /// <summary>
    /// Creates table plan buttons for selecting table plans
    /// </summary>
    public void CreateButtons()
    {
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (tablePlans.Count) * 100);
        for (int i = 0; i < tablePlans.Count; i++)
        {
            GameObject tempGameobject = Instantiate(buttonPrefab, this.gameObject.transform);
            tempGameobject.transform.localPosition = new Vector3(0, 50 - 100 * (i + 1), 0);
            tempGameobject.GetComponent<RectTransform>().offsetMax = new Vector2(0, tempGameobject.GetComponent<RectTransform>().offsetMax.y);
            tempGameobject.GetComponent<RectTransform>().offsetMin = new Vector2(0, tempGameobject.GetComponent<RectTransform>().offsetMin.y);
            tempGameobject.name = tablePlans[i].name;
            tempGameobject.transform.GetChild(0).GetComponent<Text>().text = tablePlans[i].name;
        }
    }
}
