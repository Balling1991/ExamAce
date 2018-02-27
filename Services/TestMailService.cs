using Microsoft.Extensions.Logging;

namespace ExamAce.Services
{
    public class TestMailService : IMailService
    {
        private readonly ILogger<TestMailService> _logger;

        public TestMailService(ILogger<TestMailService> logger)
        {
            _logger = logger;
        }

        public void SendEmail(string to, string subject, string body)
        {
            _logger.LogInformation($"To: {to}, Subject: {subject}, Body: {body}");
        }
    }
}
