using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Mail;
using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;

public class SendReport : MonoBehaviour
{
    public ReportsController reportsController;
    public ErrorStateCanvas error;

    /// <summary>
    /// Checks if we have any data to send and seds and email to selected recipients if we have data. You cannot send to 0 recipients
    /// </summary>
    public void OnClick()
    {
        if (reportsController.CollectEmails().Count == 0)
        {
            error.gameObject.SetActive(true);
            error.ErrorSelectAtLeastOneRecipient();
        }
        else if(CollectData.IsDataEmpty())
        {
            error.gameObject.SetActive(true);
            error.ErrorNoDataToSend();
        }
        else
        {
            SendEmail();
        }
    }

    /// <summary>
    /// Sends an email with the format and data required
    /// </summary>
    private void SendEmail()
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("g2gamingltd@gmail.com");
        foreach (string tempstring in reportsController.CollectEmails())
        {
            mail.To.Add(tempstring);
        }
        mail.Subject = $"{DateTime.Now.ToShortDateString()} Pax reports";
        mail.Body = Message();


        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

        smtpServer.Port = 587;

        smtpServer.Credentials = new NetworkCredential("g2gamingltd@gmail.com", "ozjtngzauvtnqkvu") as ICredentialsByHost;

        smtpServer.EnableSsl = true;

        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors) {
                Debug.Log("Email success!");
                return true;
            };

        // Send mail to server, print results
        try
        {
            smtpServer.Send(mail);
        }
        catch (System.Exception e)
        {
            Debug.Log("Email error: " + e.Message);
            error.gameObject.SetActive(true);
            error.ErrorReportNotSent();
        }
        finally
        {
            Debug.Log("Email sent!");
            error.gameObject.SetActive(true);
            error.ReportsSentSuccesfully();
            CollectData.ResetData();
        }
    }

    /// <summary>
    /// Here we make a message string from the data we have collected
    /// </summary>
    /// <returns></returns>
    public string Message()
    {
        string message = CollectData.GetInformation();

        return message;
    }
}
