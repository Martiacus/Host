using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTablePlanButtons : MonoBehaviour
{
    public GameObject tablePlanButtonPrefab;        // Our prefab with preset gameobjects for easier instanciation
    public GameObject data;                         // Our Global gameobject where we store all of the plans and buttons
    public GameObject errorCanvas;                  // Our error canvas that displays the error

    public int xRes;            // X Axis resolution we are working with
    public int yRes;            // Y Axis resoluton we are working with

    private int xCoords;        // How much space we are giving for 1 button on x axis
    private int yCoords;        // How much space we are giving for 1 button on y axis

    private int xCount;         // Number of buttons we can add on X axis
    private int yCount;         // Number of buttons we can add on Y axis

    /// <summary>
    /// We check all the possible combinations of numbers from 1 to table plan count that multiplied have at least 5 spaces
    /// Those spaces are then added to our score, because we want as few spaces as possible to use we use the lower the score the better
    /// Since 1 * spaces count would always be the lowest we do another test to check for numbers that are as close to each other as possible
    /// So we add to our score the difference between numbers to our score so the closer the numbers are to each other the lower the score
    /// And now we have our most optimal grid layout for that ammount of data
    /// FORMULA(count = table plans count, i = yaxis, j = xaxis(because j will be larger and we have more space on the x axis we want it always equal or larger than y axis)):
    /// if j * i >= count score = count * count + 1 (this is just a precaution to get the lowest minimmum score possible + 1) (we test if i*j provides at least enaugh spaces for us to use if not we dont need them and assign a high score)
    /// ( j * i ) + ( j - i ) = score (This is the full formula to assigning a valid score to check the best scenario) (j is always larger than i because to get the score it doesnt matter if the grid is 2x3 or 3x2 or has the same score we dont need to check again)
    /// </summary>
    private void FindXYCount()
    {
        int minScore = -1;             // We set a minscore very high so we calculate only the lowest value
        int minXValue = 0;
        int minYValue = 0;
        int tempscore;                      // Here we store our temporary score before comparing it with the minimum score

        for (int i = 1; i <= data.GetComponent<TablePlans>().selectedTablePlans.Count; i++)         // Our y axis
        {
            for (int j = i; j <= data.GetComponent<TablePlans>().selectedTablePlans.Count; j++)     // Our x axis
            {
                if (j * i < data.GetComponent<TablePlans>().selectedTablePlans.Count)
                {
                    tempscore = data.GetComponent<TablePlans>().selectedTablePlans.Count * data.GetComponent<TablePlans>().selectedTablePlans.Count +1; // We set the highest possible score +1
                }
                else
                {
                    tempscore = j * i + (j - i);        // Here we set the proper score
                }
                if(tempscore < minScore || minScore < 0)                // Checking if we got a lower score and assigning it if we ded
                {
                    minXValue = j;
                    minYValue = i;
                    minScore = tempscore;
                }
            }
        }

        //Here we assing the values to our global values we need
        xCount = minXValue;
        yCount = minYValue;
        xCoords = xRes / xCount;
        yCoords = yRes / yCount;
    }

    /// <summary>
    /// Here we check how many table plans were selected before displaying an error and closing the canvas
    /// </summary>
    public void CreateButtons()
    {
        Create();
    }

    /// <summary>
    /// Here we create our table plan buttons with their plan image generated
    /// </summary>
    private void Create()
    {
        FindXYCount();

        data.GetComponent<TablePlans>().FindOnTablePlans();

        int usedTablePlanCount = 0;             // This parameter is used in case we have less table plans but a larger grid for example 7 plans need a 3x3 9 space grid. So we dont create nothing we just finish the for functions

        GameObject tempGameobject;              // Gameobject used to manipulate the instanciated table plan button

        for (int i = 0; i < yCount; i++)
        {
            for (int j = 0; j < xCount; j++)
            {
                tempGameobject = Instantiate(tablePlanButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                tempGameobject.transform.SetParent(this.gameObject.transform);                              // Here we assign a parent for easier use
                tempGameobject.transform.localScale = new Vector3(1, 1, 1);                                 // Here we set the scale to 1 so there are no misinstructions
                tempGameobject.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);                                                     // Here we set the gameobject transform to recctransform
                tempGameobject.GetComponent<RectTransform>().offsetMin = new Vector2(j * xCoords, (yRes - ((i + 1) * yCoords)));                // Here we set left and bottom rect transform
                tempGameobject.GetComponent<RectTransform>().offsetMax = new Vector2(( xRes - ((j + 1) * xCoords)) * -1, i * yCoords * -1);     // Here we set right and top rect transform
                tempGameobject.name = data.GetComponent<TablePlans>().onTablePlans[usedTablePlanCount].GetPlanName();                           // Here we set the canvas plan name
                tempGameobject.transform.GetChild(1).gameObject.GetComponent<Text>().text = tempGameobject.name;                                // Here we set the name of the table plan
                tempGameobject.transform.GetChild(3).gameObject.GetComponent<CreateTablePlanImages>().CreateImages(data.GetComponent<TablePlans>().onTablePlans[usedTablePlanCount]);   // Here we create the images for table plan
                GameObject.Find("Controller").GetComponent<Controller>().AddGameobjectToPlanButton(tempGameobject.name, tempGameobject);        // Here we set this button to the plan in Controller so we can access it anytime
                usedTablePlanCount++;

                if (usedTablePlanCount == data.GetComponent<TablePlans>().onTablePlans.Count)               // Here we check if we reached our active tablee plan limit if we did we stop the loop
                {
                    break;
                }
            }
        }

    }
}
