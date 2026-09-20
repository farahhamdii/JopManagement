using JobApplication.Application.DTOs.Application;
using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;
using JobApplication.Domain.Enums;

namespace JobApplication.Application.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ICandidateRepository _candidateRepository;

        public ApplicationService(
            IApplicationRepository applicationRepository,
            IJobRepository jobRepository,
            ICandidateRepository candidateRepository)
        {
            _applicationRepository = applicationRepository;
            _jobRepository = jobRepository;
            _candidateRepository = candidateRepository;
        }

        public async Task ApplyAsync(
            ApplyJobRequest request,
            string userId)
        {
            var candidate = await _candidateRepository.GetByUserIdAsync(userId);

            if (candidate == null)
                throw new Exception("Candidate not found.");

            var job =await _jobRepository.GetByIdAsync(request.JobId);

            if (job == null)
                throw new Exception("Job not found.");

            if (job.IsDeleted || !job.IsActive)
                throw new Exception("You cannot apply to this job.");

            var existingApplication =await _applicationRepository.GetByCandidateAndJobAsync( candidate.Id,request.JobId);

            if (existingApplication != null)
                throw new Exception("You have already applied to this job.");

            var application = new JobCandidateApplication
            {
                CandidateId = candidate.Id,
                JobId = request.JobId,
                JobApplicationStatus = JobApplicationStatus.Applied,
                AppliedAt = DateTime.UtcNow,
                StatusUpdatedAt = DateTime.UtcNow
            };

            await _applicationRepository.AddAsync(application);
        }

        public async Task CancelAsync( int applicationId,string userId)
        {
            var candidate =await _candidateRepository.GetByUserIdAsync(userId);

            if (candidate == null)
                throw new Exception("Candidate not found.");

            var application = await _applicationRepository .GetByIdAsync(applicationId);

            if (application == null)
                throw new Exception("Application not found.");

            if (application.CandidateId != candidate.Id)
                throw new UnauthorizedAccessException("You can only cancel your own applications.");

            if (application.JobApplicationStatus !=JobApplicationStatus.Applied)
                throw new Exception("You can only cancel an application with Applied status.");

            application.JobApplicationStatus = JobApplicationStatus.Cancelled;
            application.CancelledAt = DateTime.UtcNow;
            application.StatusUpdatedAt = DateTime.UtcNow;
            await _applicationRepository.UpdateAsync(application);
        }
    }
}