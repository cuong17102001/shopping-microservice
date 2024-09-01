using BuildingBlocks.Contracts.Configurations;
using BuildingBlocks.Contracts.Services;
using BuildingBlocks.Infrastructure.Configurations;
using BuildingBlocks.Shared.Services.Email;
using MailKit.Net.Smtp;
using MimeKit;
using Serilog;

namespace Infrastructure.Services
{
    public class SmtpEmailService : IEmailService<MailRequest>
    {
        private readonly ILogger _logger;
        private readonly SMTPEmailSettings _settings;
        private readonly SmtpClient _smtpClient;

        public SmtpEmailService(ILogger logger, SMTPEmailSettings settings)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _smtpClient = new SmtpClient();
        }

        public async Task SendEmailAsync(MailRequest request, CancellationToken cancellationToken = default)
        {
            var emailMessage = new MimeMessage
            {
                Sender = new MailboxAddress(_settings.DisplayName, request.From ?? _settings.From),
                Subject = request.Subject,
                Body = new BodyBuilder
                {
                    HtmlBody = request.Body
                }.ToMessageBody()
            };

            if (request.ToAddresses.Any())
            {
                foreach (var address in request.ToAddresses)
                {
                    emailMessage.To.Add(MailboxAddress.Parse(address));
                }
            }
            else
            {
                emailMessage.To.Add(MailboxAddress.Parse(request.ToAddress));
            }
            await _smtpClient.SendAsync(emailMessage, cancellationToken);
        }
    }
}
