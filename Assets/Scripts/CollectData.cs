using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollectData
{
    private static int bookingsCount;
    private static int totalPax;
    private static int peakPax;
    private static DateTime peakPaxTime;
    private static List<PaxCount> paxCount;
    private static List<TableStay> tableStay;

    private class PaxCount
    {
        static DateTime time;
        static int pax;
        static int totalPax;

        public PaxCount(DateTime senderTime, int senderPax, int senderTotalPax)
        {
            time = senderTime;
            pax = senderPax;
            totalPax = senderTotalPax;
        }

        public DateTime GetTime()
        {
            return time;
        }

        public int GetPax()
        {
            return pax;
        }

        public int GetTotalPax()
        {
            return totalPax;
        }
    }

    private class TableStay
    {
        static int stayTime;
        static int pax;

        public TableStay(int senderStayTime, int senderPax)
        {
            stayTime = senderStayTime;
            pax = senderPax;
        }

        public int GetStayTime()
        {
            return stayTime;
        }

        public int GetPax()
        {
            return pax;
        }
    }

    /// <summary>
    /// Here we reset the data to 0 values to start collecting new data
    /// </summary>
    public static void StartCollecting()
    {
        bookingsCount = 0;
        totalPax = 0;
        peakPax = 0;
        peakPaxTime = DateTime.Now;
        paxCount = new List<PaxCount>();
        tableStay = new List<TableStay>();
    }

    /// <summary>
    /// Here we reset the data to default 0 values
    /// </summary>
    public static void ResetData()
    {
        bookingsCount = 0;
        totalPax = 0;
        peakPax = 0;
        if(paxCount.Count > 0)
        {
            paxCount.Clear();
        }
        if(tableStay.Count > 0)
        {
            tableStay.Clear();
        }
    }

    /// <summary>
    /// Returns data status true if empty and false if there is data
    /// </summary>
    /// <returns></returns>
    public static bool IsDataEmpty()
    {
        if(bookingsCount == 0 && totalPax == 0 && peakPax == 0 && paxCount.Count == 0 && tableStay.Count ==0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Here we set the total ammount of bookings we had today
    /// </summary>
    /// <param name="count"> Bookings count</param>
    public static void SetBookingsCount(int count)
    {
        bookingsCount = count;
    }

    /// <summary>
    /// Here we set the total pax that came today
    /// </summary>
    /// <param name="count"> Total pax</param>
    public static void SetTotalPax(int count)
    {
        totalPax = count;
    }

    /// <summary>
    /// Here we set the peak pax count inside at one time today
    /// </summary>
    /// <param name="count"> Peak pax count</param>
    public static void SetPeakPax(int count)
    {
        peakPax = count;
    }

    /// <summary>
    /// Here we set the time peak pax was reached
    /// </summary>
    /// <param name="time"> Time peak pax was reached</param>
    public static void SetPeakPaxTime(DateTime time)
    {
        peakPaxTime = time;
    }

    /// <summary>
    /// Here we add one timestamp of current pax and time of timestamp (ideally this will be preformed every 30 mins)
    /// </summary>
    /// <param name="time"> The timme the timestamp was taken</param>
    /// <param name="pax"> The pax during the timestamp</param>
    public static void AddPaxCount(DateTime time, int pax, int totalPax)
    {
        paxCount.Add(new PaxCount(time, pax, totalPax));
    }

    /// <summary>
    /// Here we record total single table stay time and pax
    /// </summary>
    /// <param name="stayTime"> Stay time in minutes</param>
    /// <param name="pax"> Pax count</param>
    public static void AddTableStay(int stayTime, int pax)
    {
        tableStay.Add(new TableStay(stayTime, pax));
    }

    /// <summary>
    /// Here we get all the information and make it into a string format that we will be sending to the user
    /// </summary>
    /// <returns> Collected information in string format</returns>
    public static string GetInformation()
    {
        string temp;

        temp = $"Bookings count: {bookingsCount}\nTotal pax: {totalPax}\nPeak pax: {peakPax}\nTime the peak pax was reached: {peakPaxTime.ToShortTimeString()}\n";

        temp += "Pax | Average stay for pax(in minutes)\n";
        foreach(int tempInt in MakeListOfPaxCounts())
        {
            temp += $"{tempInt, 4}  {AverageStayInPax(tempInt)}\n";
        }
        
        temp += "Time | Total pax | Current pax\n";
        foreach(PaxCount tempCount in paxCount)
        {
            temp += $"{tempCount.GetTime().ToShortTimeString(),3}{tempCount.GetTotalPax(),11}{tempCount.GetPax()}\n";
        }
        return temp;
    }

    /// <summary>
    /// Here we calcualte the average table stay in minutes with specific pax count
    /// </summary>
    /// <param name="givenPax"> The pax count we are calculating the time for</param>
    /// <returns> Average stain in minutes</returns>
    private static int AverageStayInPax(int givenPax)
    {
        int tempInt = 0;
        int tempCount = 0;

        foreach (TableStay tempStay in tableStay)
        {
            if(tempStay.GetPax() == givenPax)
            {
                tempInt += tempStay.GetStayTime();
                tempCount++;
            }
        }

        return tempInt / tempCount;
    }

    /// <summary>
    /// Here we make a list of all pax count that actually came so we dont have to send data for pax that didint come today
    /// </summary>
    /// <returns> List of pax counts that came in</returns>
    private static List<int> MakeListOfPaxCounts()
    {
        List<int> tempCount = new List<int>();

        foreach(TableStay tempStay in tableStay)
        {
            if(!tempCount.Contains(tempStay.GetPax()))
            {
                tempCount.Add(tempStay.GetPax());
            }
        }
        tempCount.Sort();

        return tempCount;
    }
}
