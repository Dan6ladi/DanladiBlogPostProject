using BlogPost.SharedKernel.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Infrastructure.Model;

namespace BlogPost.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private EmailSettings _settings;
        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public bool SendWelcomeMessage(string recipient, string firstName)
        {
            var email = _settings.Sender;
            var mailModel = EmailModel.CreateInstance(email, recipient);
            mailModel.Body = EmailTemplateHelper.WelcomeMailTemplate(firstName);
            var sendEmail = SendEmail(mailModel);
            return sendEmail;
        }


        public bool SendEmail(EmailModel model)
        {

            try
            {
                MailAddress to = new MailAddress(model.Recipient);

                MailAddress from = new MailAddress(_settings.Username);



                var mail = new MailMessage(from, to);
                mail.Subject = model.Subject;
                mail.Body = model.Body;
                mail.IsBodyHtml = true;
                using (var smtpClient = new SmtpClient(_settings.Host, _settings.Port))
                {
                    smtpClient.Credentials = new NetworkCredential(_settings.Username, _settings.Password);
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mail);
                    smtpClient.Dispose();

                }

                return true;

            }

            catch (Exception ex)

            {
                Console.WriteLine($"An Exception Occurred while trying to send Email ---- {ex.Message} \n {ex.StackTrace}");

                return false;

            }

        }
    }
}
