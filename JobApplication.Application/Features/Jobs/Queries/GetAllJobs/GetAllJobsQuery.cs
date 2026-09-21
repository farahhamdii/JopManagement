using JobApplication.Application.DTOs.Job;
using MediatR;

namespace JobApplication.Application.Features.Jobs.Queries.GetAllJobs
{
    public class GetAllJobsQuery : IRequest<IEnumerable<JobResponse>>
    {
    }
}