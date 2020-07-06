using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecreateTablePlanButtons : MonoBehaviour
{
    /// <summary>
    /// Recreates all table plan buttons after deleting a single unit to not leave gaps
    /// </summary>
    public void OnClick()
    {
        this.gameObject.transform.parent.parent.gameObject.GetComponent<CreateTablePlansSelectionButtons>().OnEnabling();
    }
}
