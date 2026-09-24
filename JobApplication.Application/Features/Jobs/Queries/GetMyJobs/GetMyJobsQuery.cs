using JobApplication.Application.DTOs.Job;
using MediatR;

namespace JobApplication.Application.Features.Jobs.Queries.GetMyJobs
{
    public class GetMyJobsQuery : IRequest<IEnumerable<JobResponse>>
    {
        public string RecruiterId { get; set; }

        public GetMyJobsQuery(string recruiterId)
        {
            RecruiterId = recruiterId;
        }
    }
}