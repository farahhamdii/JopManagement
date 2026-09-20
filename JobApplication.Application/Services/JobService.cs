using JobApplication.Application.DTOs.Job;
using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;

namespace JobApplication.Application.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task CreateJobAsync( CreateJobRequest request, string recruiterId)
        {
            var job = new Job
            {
                Title = request.Title,
                Description = request.Description,
                RecruiterId = recruiterId,
                IsActive = true,
                IsDeleted = false
            };

            await _jobRepository.AddAsync(job);
        }

        public async Task CancelJobAsync(int jobId, string recruiterId)
        {
            var job = await _jobRepository.GetByIdAsync(jobId);
            if (job == null)
                throw new Exception("Job not found.");
            if (job.RecruiterId != recruiterId)
                throw new UnauthorizedAccessException("You can only cancel your own jobs.");

            if (job.IsDeleted)
                throw new Exception("Job is already cancelled.");

            job.IsActive = false;
            job.IsDeleted = true;
            job.DeletedAt = DateTime.UtcNow;

            await _jobRepository.UpdateAsync(job);
        }
    }
}