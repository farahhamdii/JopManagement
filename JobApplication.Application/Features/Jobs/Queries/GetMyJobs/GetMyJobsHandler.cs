using JobApplication.Application.DTOs.Job;
using JobApplication.Application.Interfaces.Repositories;
using MediatR;

namespace JobApplication.Application.Features.Jobs.Queries.GetMyJobs
{
    public class GetMyJobsHandler : IRequestHandler<GetMyJobsQuery, IEnumerable<JobResponse>>
    {
        private readonly IJobRepository _jobRepository;

        public GetMyJobsHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<IEnumerable<JobResponse>> Handle(GetMyJobsQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetByRecruiterIdAsync(request.RecruiterId);

            return jobs.Select(job => new JobResponse
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                RecruiterId = job.RecruiterId,
                IsActive = job.IsActive
            });
        }
    }
}