using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToPrieviewCanvas : MonoBehaviour
{
    GameObject previewCanvas;

    /// <summary>
    /// Here we find the preview canvas through the hierarchy and children
    /// </summary>
    void Start()
    {
        previewCanvas = this.gameObject.transform.parent.parent.parent.parent.parent.GetChild(5).gameObject;
    }

    /// <summary>
    /// Here we enable the preview canvas and start its scripts
    /// </summary>
    public void OnClick()
    {
        previewCanvas.SetActive(true);
        previewCanvas.transform.GetChild(3).gameObject.GetComponent<CreateButtonLayoutForPreview>().OnClick(this.gameObject.transform.parent.name);
        previewCanvas.transform.GetChild(1).gameObject.GetComponent<Text>().text = this.gameObject.transform.parent.name;

    }
}
