﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTablePlanImages : MonoBehaviour
{
    public GameObject wallPrefab;       // Our wall prefab
    public GameObject ButtonPrefab;     // Our button prefab

    private float xSize;                // Our screen X coordinetes size
    private float ySize;                // Our screen Y coordinetes size

    /// <summary>
    /// Creates all the images for table plan button
    /// </summary>
    /// <param name="data"> A single table plan</param>
    public void CreateImages(TablePlan data)
    {
        xSize = this.gameObject.GetComponent<RectTransform>().rect.width / data.GetXAxisCount();
        ySize = this.gameObject.GetComponent<RectTransform>().rect.height / data.GetYAxisCount();


        foreach (CollectedButtonsClass button in data.GetCollectedButtons())
        {
            if(button.GetColorID() == 1)
            {
                CreatePrefab(wallPrefab, button, false);
            }
            else if(button.GetColorID() == 2)
            {
                CreatePrefab(ButtonPrefab, button, true);
            }
        }
    }

    /// <summary>
    /// Here we instanciate the appropiate prefab with the given data
    /// </summary>
    /// <param name="prefab"> The prefab we will instanciate</param>
    /// <param name="data"> Here we have the collected button info that we are instanciating</param>
    /// <param name="isItATable"> Here we declare if the prefab is a table or a wall for table specific scripts</param>
    private void CreatePrefab(GameObject prefab, CollectedButtonsClass data, bool isItATable)
    {
        GameObject tempGameobject = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity); // Here we create our wall or button
        tempGameobject.transform.SetParent(this.gameObject.transform);                              // Here we assign a parent for easier use
        tempGameobject.transform.localScale = new Vector3(1, 1, 1);                                 // Here we set the scale to 1 so there are no misinstructions
        tempGameobject.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);                 // Here we set the gameobject transform to recctransform
        tempGameobject.GetComponent<RectTransform>().offsetMin = new Vector2(data.GetXCoordinate() * xSize, this.gameObject.GetComponent<RectTransform>().rect.height - ((data.GetYCoordinate() + 1) * ySize));                 // Here we set left and bottom rect transform
        tempGameobject.GetComponent<RectTransform>().offsetMax = new Vector2(-1 * (this.gameObject.GetComponent<RectTransform>().rect.width - (xSize * (data.GetXCoordinate() + 1))) , -1 * data.GetYCoordinate() * ySize);     // Here we set right and top rect transform
        if (isItATable) // We assign a name depending if it is a table or wall
        {
            tempGameobject.name = "Table " + data.GetXCoordinate() + "-" + data.GetYCoordinate();
            GameObject.Find("Controller").GetComponent<Controller>().AddTableImage(tempGameobject.transform.parent.parent.name, tempGameobject.name, tempGameobject);   // Here we assign the image to the controller to be able to acces it anytime and change color depending on the table availability
        }
        else
        {
            tempGameobject.name = "Wall " + data.GetXCoordinate() + "-" + data.GetYCoordinate();
        }
    }
}
