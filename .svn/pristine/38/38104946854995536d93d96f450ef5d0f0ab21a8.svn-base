using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class Helper
    {
        public static bool SendEmail(string EmailId, string Subject, string Body)
        {
            //string from = "support@totalbusinessplus.com"; //from address    
            //string to = EmailId; //to address    
            //MailMessage message = new MailMessage(from, to);

            //string mailbody = Subject;
            //message.Subject = "Hello";
            //message.Body = mailbody;
            //message.BodyEncoding = Encoding.UTF8;
            //message.IsBodyHtml = true;

            //SmtpClient client = new SmtpClient("relay-hosting.secureserver.net",25); //Gmail smtp   
            //System.Net.NetworkCredential basicCredential1 = new
            //System.Net.NetworkCredential("support@totalbusinessplus.com", "tlrrnffyxmaopahz");
            //client.EnableSsl = false;
            //client.UseDefaultCredentials = false;
            //client.Credentials = basicCredential1;
            //try
            //{
            //    client.Send(message);
            //}

            //catch (Exception e)
            //{
            //    throw e;
            //} 

            try
            {
                MailMessage mail = new MailMessage("support@totalbusinessplus.com", EmailId, "", "");
                System.Net.Mime.ContentType mimeType = new System.Net.Mime.ContentType("text/html");

                mail.Subject = Subject;
                mail.Body = Body;
                mail.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                mail.IsBodyHtml = true;
                SmtpClient oSmtpClient = new SmtpClient();

                //oSmtpClient.Credentials = new System.Net.NetworkCredential("sales@dhitsolutions.com", "Sales1!");
                //oSmtpClient.Host = "relay-hosting.secureserver.net";
                //oSmtpClient.Port = 25;
                //oSmtpClient.EnableSsl = true;

                oSmtpClient.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }

        }
    }
}
