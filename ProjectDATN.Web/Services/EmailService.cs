﻿using System.Net.Mail;
using System.Net;

namespace ProjectDATN.Web.Services
{
    public class EmailService : IEmailService
    {
        public bool Send(string name, string subject, string content, string toMail)
        {
            bool rs = false;
            string email = "ndhshop.shop@gmail.com";
            string password = "abmcstqyjqlmiqlg";

            try
            {
                MailMessage message = new MailMessage();
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;

                    smtp.Credentials = new NetworkCredential()
                    {
                        UserName = email,
                        Password = password
                    };

                }
                MailAddress fromAddress = new MailAddress(email, name);
                message.From = fromAddress;
                message.To.Add(toMail);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = content;
                smtp.Send(message);

                rs = true;
            }
            catch (Exception)
            {

            }
            return rs;
        }
    }
}
