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
            var mailMessage = new MailMessage("ningluo89@gmail.com", toEmail, subject, body);

            var smtpClient = new SmtpClient();

            // Uncomment this to send email, and change the password in Web.config
            //smtpClient.Send(mailMessage);

            return true;
        }
    }
}