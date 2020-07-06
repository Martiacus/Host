using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationsController : MonoBehaviour
{
    public Text notification;
    public MainHostController mainHostController;

    /// <summary>
    /// Here we start the notification
    /// </summary>
    /// <param name="text"></param>
    public void StartNotifications(string text)
    {
        notification.text = text;
    }

    /// <summary>
    /// Here we close the notification and if we have more we receive a new one
    /// </summary>
    public void CloseNotification()
    {
        mainHostController.DecreaseNotificationQueue();
    }
}
