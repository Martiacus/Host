using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollectedButtonsClass
{
    private int xCoordinate;
    private int yCoordinate;
    private int colorID;

    /// <summary>
    /// Here we add the data to collected buttons class of a single table
    /// </summary>
    /// <param name="x"> X coordinate value in a grid</param>
    /// <param name="y"> Y coordinate value in a grid</param>
    /// <param name="color"> Color ID of the button</param>
    public CollectedButtonsClass(int x, int y, int color)
    {
        xCoordinate = x;
        yCoordinate = y;
        colorID = color;
    }

    /// <summary>
    /// Here we set the coordinate values of the button
    /// </summary>
    /// <param name="x"> Rows value</param>
    /// <param name="y"> Columns value</param>
    public void SetCoordinates(int x, int y)
    {
        xCoordinate = x;
        yCoordinate = y;
    }

    /// <summary>
    /// Here we set the table color ID
    /// </summary>
    /// <param name="id"> Color id</param>
    public void SetColorID(int id)
    {
        colorID = id;
    }

    /// <summary>
    /// Here we retrun the X coordinate value of the table
    /// </summary>
    /// <returns> Row number</returns>
    public int GetXCoordinate()
    {
        return xCoordinate;
    }

    /// <summary>
    /// Here we return the Y coordinate value of the table
    /// </summary>
    /// <returns> Column number</returns>
    public int GetYCoordinate()
    {
        return yCoordinate;
    }

    /// <summary>
    /// Here we return the color ID of the table
    /// </summary>
    /// <returns> Color ID</returns>
    public int GetColorID()
    {
        return colorID;
    }
}
