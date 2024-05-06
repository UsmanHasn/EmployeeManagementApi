using BusinesObjectLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BusinessLogicLayer.Helper
{
    //added by usman
    public interface IEmailHelper
    {
        Task SendEmailAsync(List<string> recipients, BOL_EmailData emailData);
    }
    public class EmailHelper : IEmailHelper
    {
        private readonly IConfiguration _IConfiguration;

        public EmailHelper(IConfiguration IConfiguration)
        {
            _IConfiguration = IConfiguration;
        }
        public async Task SendEmailAsync(List<string> recipients, BOL_EmailData emailData)
        {
            using (var message = new MailMessage())
            {
                foreach (var recipient in recipients)
                {
                    message.To.Add(recipient);
                }

                message.From = new MailAddress(_IConfiguration["EmailConfig:FromEmail"], _IConfiguration["EmailConfig:DisplayName"]);
                message.Subject = emailData.EmailSubject;

                message.Body = emailData.EmailBody;
                message.IsBodyHtml = true;
                using (var smtpClient = new SmtpClient(_IConfiguration["EmailConfig:Host"],Convert.ToInt32(_IConfiguration["EmailConfig:Port"])))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(_IConfiguration["EmailConfig:FromEmail"], _IConfiguration["EmailConfig:FromPassword"]);
                    smtpClient.EnableSsl = true;

                    await smtpClient.SendMailAsync(message);
                }
            }
        }
    }

}

