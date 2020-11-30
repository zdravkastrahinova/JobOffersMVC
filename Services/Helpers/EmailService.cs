using JobOffersMVC.ViewModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;

namespace JobOffersMVC.Services.Helpers
{
    public class EmailService : IEmailService
    {
        private EmailSettings settings;

        public EmailService(AppSettings appSettings)
        {
            settings = appSettings.EmailSettings;
        }

        public async Task SendAsync(EmailViewModel model)
        {
            // From
            MailboxAddress from = new MailboxAddress(settings.EmailName, settings.EmailAccount);

            // To
            MailboxAddress to = new MailboxAddress(model.UserName, model.UserEmail);

            // Content
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = model.Body;

            MimeMessage message = new MimeMessage
            {
                Subject = model.Subject,
                Body = bodyBuilder.ToMessageBody()
            };

            message.From.Add(from);
            message.To.Add(to);

            // Client for sending email
            SmtpClient client = new SmtpClient();

            client.Connect(settings.Host, settings.Port, SecureSocketOptions.StartTls);
            client.Authenticate(settings.EmailAccount, settings.EmailPassword);

            await client.SendAsync(message);

            client.Disconnect(true);
            client.Dispose();
        }
    }
}
