using JobApplication.Application.DTOs.Job;
using JobApplication.Application.Interfaces.Repositories;
using MediatR;

namespace JobApplication.Application.Features.Jobs.Queries.GetJobById
{
    public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, JobResponse>
    {
        private readonly IJobRepository _jobRepository;

        public GetJobByIdQueryHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<JobResponse> Handle( GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.Id);
            if (job == null)
                throw new Exception("Job not found.");
            return new JobResponse
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                RecruiterId = job.RecruiterId,
                IsActive = job.IsActive
            };
        }
    }
}