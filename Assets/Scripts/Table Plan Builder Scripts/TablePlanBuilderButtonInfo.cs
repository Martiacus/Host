using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TablePlanBuilderButtonInfo : MonoBehaviour
{
    public int xCoordinate;     // X value of this gameobjects coordinate
    public int yCoordinate;     // Y value of this gameobjects coordinate
    public int colorID;         // Color ID: 0 = White - nothing, 1 = Black - obstruction, 2 = Green - table;

    /// <summary>
    /// Here we set the x and y coordinates that we will be using for the buttons
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetCoordinates(int x, int y)
    {
        xCoordinate = x;
        yCoordinate = y;
    }

    /// <summary>
    /// Here we set the color Id of the buttons
    /// </summary>
    /// <param name="id"> Color ID</param>
    public void SetColorID(int id)
    {
        colorID = id;
    }

    /// <summary>
    /// Here we get the color id of this button
    /// </summary>
    /// <returns> Button color ID</returns>
    public int GetColorID()
    {
        return colorID;
    }

    /// <summary>
    /// Here we get the X coordinate of this button
    /// </summary>
    /// <returns> Button X coordinate</returns>
    public int GetXCoordinate()
    {
        return xCoordinate;
    }

    /// <summary>
    /// Here we get the Y coordinate of the button
    /// </summary>
    /// <returns> Button Y coordinate</returns>
    public int GetYCoordiante()
    {
        return yCoordinate;
    }

}
