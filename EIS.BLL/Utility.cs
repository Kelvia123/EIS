using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace EIS.BLL
{
    public class Utility
    {
        public static bool SendMail(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,  // Gmail uses https, so ssl is true
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("ningluo89@gmail.com", "my gmail password") // Change to my password here
            };

            var mailMessage = new MailMessage("ningluo89@gmail.com", toEmail, subject, body);

            // Uncomment this to send email
            //smtpClient.Send(mailMessage);

            return true;
        }
    }
}