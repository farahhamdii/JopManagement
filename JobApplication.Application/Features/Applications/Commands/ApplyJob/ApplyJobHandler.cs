using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Hangfire;
using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;
using JobApplication.Domain.Enums;
using MediatR;

namespace JobApplication.Application.Features.Applications.Commands.ApplyJob
{
    public class ApplyJobHandler : IRequestHandler<ApplyJobCommand>
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IBackgroundJobScheduler _backgroundJobScheduler;

        public ApplyJobHandler(
            IApplicationRepository applicationRepository,
            IJobRepository jobRepository,
            ICandidateRepository candidateRepository,
            IBackgroundJobScheduler backgroundJobScheduler)
        {
            _applicationRepository = applicationRepository;
            _jobRepository = jobRepository;
            _candidateRepository = candidateRepository;
            _backgroundJobScheduler = backgroundJobScheduler;
        }

        public async Task Handle(ApplyJobCommand request,CancellationToken cancellationToken)
        {
            var candidate =await _candidateRepository.GetByUserIdAsync(request.UserId);

            if (candidate == null)
                throw new Exception("Candidate not found.");

            var job=await _jobRepository.GetByIdAsync(request.Request.JobId);

            if (job == null)
                throw new Exception("Job not found.");

            if (job.IsDeleted || !job.IsActive)
                throw new Exception("You cannot apply to this job.");
            var existingApplication = await _applicationRepository.GetByCandidateAndJobAsync( candidate.Id,request.Request.JobId);
            if (existingApplication != null)
                throw new Exception("You have already applied to this job.");

            var application = new JobCandidateApplication
            {
                CandidateId = candidate.Id,
                JobId = request.Request.JobId,
                JobApplicationStatus = JobApplicationStatus.Applied,
                AppliedAt = DateTime.UtcNow,
                StatusUpdatedAt = DateTime.UtcNow
            };

            await _applicationRepository.AddAsync(application);

            _backgroundJobScheduler.Enqueue<INotificationService>(x => x.NotifyRecruiterAsync(application.Id));
        }
    }
}