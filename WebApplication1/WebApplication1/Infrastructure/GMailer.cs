using System.Net;
using System.Net.Mail;
/// <summary>
/// Class representation of an automatic mail service
/// </summary>
public class GMailer
{
    /// <summary>
    /// The username of the Gmail account.
    /// </summary>
    public static string GmailUsername { get; set; }
    /// <summary>
    /// The password of the Gmail account.
    /// </summary>
    public static string GmailPassword { get; set; }
    /// <summary>
    /// String representation of the Gmail host.
    /// </summary>
    public static string GmailHost { get; set; }
    /// <summary>
    /// Port used for mail delivery.
    /// </summary>
    public static int GmailPort { get; set; }
    /// <summary>
    /// Whether the smtpClient used SSL to encrypt the connection.
    /// </summary>
    public static bool GmailSSL { get; set; }
    /// <summary>
    /// String representing mail recipient 
    /// </summary>
    public string ToEmail { get; set; }
    /// <summary>
    /// Email topic/subject
    /// </summary>
    public string Subject { get; set; }
    /// <summary>
    /// Body/content of Email
    /// </summary>
    public string Body { get; set; }
    /// <summary>
    /// Whether the mail message body is in HTML form.
    /// </summary>
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