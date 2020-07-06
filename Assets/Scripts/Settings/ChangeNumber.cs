using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNumber : MonoBehaviour
{
    public Text data;

    public int minimumValue;

    /// <summary>
    /// Increases the data number value by one
    /// </summary>
    public void IncreaseByOne()
    {
        data.text = (int.Parse(data.text) + 1).ToString();
    }

    /// <summary>
    /// Decreases the data value by one up to the minimum set value
    /// </summary>
    public void DecreaseByOne()
    {
        if(int.Parse(data.text) > minimumValue)
        {
            data.text = (int.Parse(data.text) - 1).ToString();
        }
    }
}
