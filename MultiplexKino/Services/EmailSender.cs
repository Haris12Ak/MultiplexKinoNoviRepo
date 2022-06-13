using Microsoft.Extensions.Options;
using MultiplexKino.Settings;
using System.Net;
using System.Net.Mail;

namespace MultiplexKino.Services
{
    public interface IEmailSender
    {
        Task SendEmail(string email, string subject, string htmlMessage);
    }

    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public Task SendEmail(string email, string subject, string htmlMessage)
        {
            try
            {
                using (var client = new SmtpClient
                {
                    Host = _emailSettings.MailServer,
                    Port = _emailSettings.MailPort,
                    EnableSsl = _emailSettings.EnableSsl,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword)
                })
                {
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName)
                    };

                    mailMessage.To.Add(email);
                    mailMessage.Subject = subject;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = htmlMessage;
                    client.Send(mailMessage);
                };
            }
            catch (Exception ex)
            {
                // TODO: handle exception
                // throw new InvalidOperationException(ex.Message);
            }

            return Task.FromResult(0);
        }
    }
}
