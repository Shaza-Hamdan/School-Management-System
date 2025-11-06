using System.Net;
using System.Net.Mail;
using TRIAL.Persistence;
using Microsoft.Extensions.Options;

namespace EmailSending
{
    public class Emailsending
    {
        private readonly EmailSettings emailsettings;

        public Emailsending(IOptions<EmailSettings> emailSettings)
        {
            emailsettings = emailSettings.Value;
        }
        public async Task SendEmail(string to, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient(emailsettings.SmtpServer)
                {
                    Port = emailsettings.SmtpPort,
                    Credentials = new NetworkCredential(emailsettings.SmtpUsername, emailsettings.SmtpPassword),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(emailsettings.FromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(to);

                smtpClient.Send(mailMessage);
            }

            catch (SmtpException ex)
            {
                // Handle general SMTP exceptions, like server issues
                Console.WriteLine($"SMTP error: {ex.Message}");
                throw;
            }

            catch (Exception ex)
            {
                // Handle any other exception that might occur
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }
    }
}