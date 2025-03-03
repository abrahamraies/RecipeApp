using Microsoft.Extensions.Options;
using RecipeApp.Application.Interfaces;
using RecipeApp.Application.Settings;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace RecipeApp.Application.Services
{
    public class EmailService(IOptions<SendGridSettings> sendGridSettings) : IEmailService
    {
        private readonly string _apiKey = sendGridSettings.Value.ApiKey;
        private readonly string _fromEmail = sendGridSettings.Value.FromEmail;
        private readonly string _fromName = sendGridSettings.Value.FromName;

        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_fromEmail, _fromName);
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: content, htmlContent: content);

            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to send email.");
            }
        }
    }
}
