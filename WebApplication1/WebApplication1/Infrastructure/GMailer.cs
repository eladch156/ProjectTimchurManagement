﻿using System.Net;
using System.Net.Mail;
/// <summary>
/// Class representation of an automatic mail service
/// </summary>
public class GMailer
{
    public static string GmailUsername { get; set; }
    public static string GmailPassword { get; set; }
    public static string GmailHost { get; set; }
    public static int GmailPort { get; set; }
    public static bool GmailSSL { get; set; }
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public bool IsHtml { get; set; }

    /// <summary>
    /// static constructor of the mail service. Properties assigned externally.
    /// </summary>
    static GMailer()
    {
        GmailHost = "smtp.gmail.com";
        GmailPort = 25; // Gmail can use ports 25, 465 & 587; but must be 25 for medium trust environment.
        GmailSSL = true;
    }

    /// <summary>
    /// Sends an email message via the properties assigned to the object.
    /// </summary>
    public void Send()
    {
        SmtpClient smtp = new SmtpClient();
        smtp.Host = GmailHost;
        smtp.Port = GmailPort;
        smtp.EnableSsl = GmailSSL;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential(GmailUsername, GmailPassword);

        using (var message = new MailMessage(GmailUsername, ToEmail))
        {
            message.Subject = Subject;
            message.Body = Body;
            message.IsBodyHtml = IsHtml;
            smtp.Send(message);
        }
    }
}