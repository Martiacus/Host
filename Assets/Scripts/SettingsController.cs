using UnityEngine;

public static class SettingsController
{
    private static int tablePlanBuilderWidth;
    private static int tablePlanBuilderHeight;
    private static int timeForTableStayPerPerson;
    private static int extraTableStayTime;

    /// <summary>
    /// Sets default settings values
    /// </summary>
    public static void SetDefaultValues()
    {
        tablePlanBuilderHeight = 10;
        tablePlanBuilderWidth = 15;
        timeForTableStayPerPerson = 15;
        extraTableStayTime = 0;
    }

    /// <summary>
    /// Retruns the table plan builder width length
    /// </summary>
    /// <returns> Width length</returns>
    public static int GetTablePlanBuilderWidth()
    {
        return tablePlanBuilderWidth;
    }

    /// <summary>
    /// Returns extra table stay time
    /// </summary>
    /// <returns>extra table stay time</returns>
    public static int GetExtraTableStayTime()
    {
        return extraTableStayTime;
    }

    /// <summary>
    /// Returns the table plan builder height length
    /// </summary>
    /// <returns> Height length</returns>
    public static int GetTablePlanBuilderHeight()
    {
        return tablePlanBuilderHeight;
    }

    /// <summary>
    /// Sets the table plan builder width and height length
    /// </summary>
    /// <param name="width"> Width length</param>
    /// <param name="height"> Height length</param>
    public static void SetTablePlanBuilderWidthAndHeight(int width, int height)
    {
        tablePlanBuilderWidth = width;
        tablePlanBuilderHeight = height;
    }

    /// <summary>
    /// Here we set the projected table stay per person time
    /// </summary>
    /// <param name="time"> Time the group will stay per person</param>
    public static void SetTableStayPerPersonTime(int time)
    {
        timeForTableStayPerPerson = time;
    }

    /// <summary>
    /// Sets the table stay extension in "time" minutes
    /// </summary>
    /// <param name="time"> Time in minutes</param>
    public static void SetTableStayExtensionTime(int time)
    {
        extraTableStayTime = time;
    }

    /// <summary>
    /// Here we get the time that a grou will stay calcuated per person
    /// </summary>
    /// <returns> Time the grou will stay per person</returns>
    public static int GetProjectedTableStayPerPersonTime()
    {
        return timeForTableStayPerPerson;
    }
}
