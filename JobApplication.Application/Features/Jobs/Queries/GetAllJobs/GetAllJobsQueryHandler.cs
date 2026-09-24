using JobApplication.Application.DTOs.Common;
using JobApplication.Application.DTOs.Job;
using JobApplication.Application.Interfaces.Repositories;
using MediatR;

namespace JobApplication.Application.Features.Jobs.Queries.GetAllJobs
{
    public class GetAllJobsHandler :
        IRequestHandler<GetAllJobsQuery, PagedResult<JobResponse>>
    {
        private readonly IJobRepository _jobRepository;

        public GetAllJobsHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<PagedResult<JobResponse>> Handle(
            GetAllJobsQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _jobRepository.GetAllAsync(
                request.Filter);

            var jobs = result.Jobs.Select(job => new JobResponse
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                RecruiterId = job.RecruiterId,
                IsActive = job.IsActive
            });

            var totalPages = (int)Math.Ceiling( (double)result.TotalCount / request.Filter.PageSize);

            return new PagedResult<JobResponse>
            {
                Items = jobs,
                PageNumber = request.Filter.PageNumber,
                PageSize = request.Filter.PageSize,
                TotalCount = result.TotalCount,
                TotalPages = totalPages
            };
        }
    }
}