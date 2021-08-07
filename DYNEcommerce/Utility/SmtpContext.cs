using System;
using System.Net;
using System.Net.Mail;

namespace DYNEcommerce.Utility
{
    public class SmtpContext
    {

        public SmtpContext(EmailAccount account)
        {

            this.Host = account.Host;
            this.Port = account.Port;
            this.EnableSsl = account.EnableSsl;
            this.Password = account.Password;
            this.UseDefaultCredentials = account.UseDefaultCredentials;
            this.Username = account.Username;
        }

        public bool UseDefaultCredentials
        {
            get;
            set;
        }

        public string Host
        {
            get;
            set;
        }

        public int Port
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public bool EnableSsl
        {
            get;
            set;
        }

        public SmtpClient ToSmtpClient()
        {
            var smtpClient = new SmtpClient(this.Host, this.Port);

            smtpClient.UseDefaultCredentials = this.UseDefaultCredentials;
            smtpClient.EnableSsl = this.EnableSsl;
            smtpClient.Timeout = 10000;

            if (this.UseDefaultCredentials)
            {
                smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
            }
            else
            {
                if (!String.IsNullOrEmpty(this.Username))
                    smtpClient.Credentials = new NetworkCredential(this.Username, this.Password);
            }

            return smtpClient;
        }

    }
}