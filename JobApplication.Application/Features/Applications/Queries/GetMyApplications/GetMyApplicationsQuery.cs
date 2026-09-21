using JobApplication.Application.DTOs.Application;
using MediatR;

namespace JobApplication.Application.Features.Applications.Queries.GetMyApplications
{
    public class GetMyApplicationsQuery : IRequest<IEnumerable<JobApplicationResponse>>
    {
        public string UserId { get; set; }

        public GetMyApplicationsQuery(string userId)
        {
            UserId = userId;
        }
    }
}