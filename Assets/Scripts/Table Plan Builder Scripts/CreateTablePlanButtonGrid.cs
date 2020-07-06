using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTablePlanButtonGrid : MonoBehaviour
{
    public int xAxisCount;                              // We input how many X axis buttons we want for the grid
    public int yAxisCount;                              // We input how many X axis buttons we want for the grid

    public Button buttonToDuplicate;                    // Here we put the button we will duplicate (saves time writing code)

    private int xResolution = 1200;                     // We input the canvas resolution manually just in case there is an error with screen sizes
    private int yResolution = 650;

    public void SetGridHeightAndWidth()
    {
        xAxisCount = SettingsController.GetTablePlanBuilderWidth();
        yAxisCount = SettingsController.GetTablePlanBuilderHeight();
    }

    public int GetTablePlanBuilderWidth()
    {
        return xAxisCount;
    }

    public int GetTablePlanBuilderHeight()
    {
        return yAxisCount;
    }

    /// <summary>
    /// Here we create the buttons
    /// </summary>
    public void MakeGrid()
    {
        // Here we count how big the button has to be to fit all of them nicelly
        int xaxis = xResolution / xAxisCount;
        int yaxis = yResolution / yAxisCount;

        Button tempButton;      // Temporary button used to manipulate instanciated original button

        //Here we create the buttons
        for (int i = 0; i < xAxisCount; i++)
        {
            for (int j = 0; j < yAxisCount; j++)
            {
                tempButton = Instantiate(buttonToDuplicate, new Vector3(0, 0, 0), Quaternion.identity);       // Here we give the temp button the power to manipulate the instantiated button
                tempButton.transform.SetParent(this.gameObject.transform);      // Here we assign a parent for easier use
                tempButton.transform.localScale = new Vector3(1, 1, 1);         // Here we set the scale to 1 so there are no misinstructions
                tempButton.GetComponent<RectTransform>().offsetMin = new Vector2(i * xaxis, yResolution - (j * yaxis + yaxis));     // Here we set left and bottom rect transform
                tempButton.GetComponent<RectTransform>().offsetMax = new Vector2(i * xaxis - xResolution + xaxis, j * yaxis * -1);  // Here we set right and top rect transform
                tempButton.GetComponent<TablePlanBuilderButtonInfo>().SetCoordinates(i,j);      // Here we set the coordinates on the button info file
            }
        }
    }
}
