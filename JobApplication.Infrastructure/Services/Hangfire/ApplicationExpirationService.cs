using JobApplication.Domain.Enums;
using JobApplication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobApplication.Infrastructure.Services.Hangfire
{
    public class ApplicationExpirationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ApplicationExpirationService> _logger;

        public ApplicationExpirationService(
            ApplicationDbContext context,
            ILogger<ApplicationExpirationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CloseExpiredApplicationsAsync()
        {
            var expirationDate = DateTime.UtcNow.AddDays(-7);

            var applications = await _context.JobCandidateApplications
                .Where(a => a.JobApplicationStatus == JobApplicationStatus.UnderReview &&
                    a.StatusUpdatedAt <= expirationDate).ToListAsync();

            foreach (var application in applications)
            {
                application.JobApplicationStatus =JobApplicationStatus.Rejected;
                application.StatusUpdatedAt =DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();
            _logger.LogInformation("Expired applications job executed. {Count} applications rejected.",
                applications.Count);
        }
    }
}