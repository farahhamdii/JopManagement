using JobApplication.Application.DTOs.Common;
using JobApplication.Application.DTOs.Job;
using MediatR;

namespace JobApplication.Application.Features.Jobs.Queries.GetAllJobs
{
    public class GetAllJobsQuery : IRequest<PagedResult<JobResponse>>
    {
        public JobFilterRequest Filter { get; set; }

        public GetAllJobsQuery(JobFilterRequest filter)
        {
            Filter = filter;
        }
    }
}