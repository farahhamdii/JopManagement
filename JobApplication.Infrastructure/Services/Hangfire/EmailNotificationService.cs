using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Hangfire;
using JobApplication.Application.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace JobApplication.Infrastructure.Services.Hangfire
{
    public class EmailNotificationService : INotificationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ILogger<EmailNotificationService> _logger;

        public EmailNotificationService(
            IApplicationRepository applicationRepository,
            ILogger<EmailNotificationService> logger)
        {
            _applicationRepository = applicationRepository;
            _logger = logger;
        }

        public async Task NotifyRecruiterAsync(int applicationId)
        {
            var application =await _applicationRepository.GetByIdAsync(applicationId);

            if (application == null)
            {
                _logger.LogWarning("Application {ApplicationId} is not found",applicationId);
                return;
            }

            _logger.LogInformation("Notification sent to recruiter for Application {ApplicationId}", applicationId);
        }
    }
}