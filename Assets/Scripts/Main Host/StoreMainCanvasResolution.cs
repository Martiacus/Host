using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreMainCanvasResolution : MonoBehaviour
{
    public int height;          // Here we have thei height of our main resolution
    public int width;           // Here we have thei height of our main resolution

    /// <summary>
    /// Returns the height value of the main screen
    /// </summary>
    /// <returns> Height</returns>
    public int GetHeight()
    {
        return height;
    }

    /// <summary>
    /// Returns the width value of the main screen
    /// </summary>
    /// <returns> Width</returns>
    public int GetWidth()
    {
        return width;
    }
}
