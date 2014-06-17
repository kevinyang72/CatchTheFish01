using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;

namespace Core.Messaging
{
    public class Messaging
    {
        public static void SendEmailGmail(string subject, string messageText)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("kevinyangstockalert@gmail.com", "Asdf!2345"),
                EnableSsl = true,
            };
            var message = new MailMessage();
            message.Body = messageText;
            // QuotedPrintable encoding is the default, but will often lead to trouble, 
            // so you should set something meaningful here. Could also be ASCII or some ISO
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            // No Subject usually goes to spam, too
            message.Subject = subject;
            // Note that you can add multiple recipients, bcc, cc rec., etc. Using the 
            // address-only syntax, i.e. w/o a readable name saves you from some issues

            AlternateView av = AlternateView.CreateAlternateViewFromString(messageText, null, MediaTypeNames.Text.Html); 
            message.AlternateViews.Add(av);
            message.To.Add("kevinyangstockalert@gmail.com");
            message.From = new MailAddress("kevinyangstockalert@gmail.com");

            client.Send(message);
        }
    }
}
