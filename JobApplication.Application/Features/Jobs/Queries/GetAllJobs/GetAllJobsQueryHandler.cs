using JobApplication.Application.DTOs.Job;
using JobApplication.Application.Interfaces.Repositories;
using MediatR;

namespace JobApplication.Application.Features.Jobs.Queries.GetAllJobs
{
    public class GetAllJobsQueryHandler: IRequestHandler<GetAllJobsQuery, IEnumerable<JobResponse>>
    {
        private readonly IJobRepository _jobRepository;

        public GetAllJobsQueryHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<IEnumerable<JobResponse>> Handle( GetAllJobsQuery request,
            CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetAllAsync();
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