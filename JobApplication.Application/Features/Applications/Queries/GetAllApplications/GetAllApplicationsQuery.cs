using JobApplication.Application.DTOs.Application;
using JobApplication.Application.DTOs.Common;
using MediatR;

namespace JobApplication.Application.Features.Applications.Queries.GetAllApplications
{
    public class GetAllApplicationsQuery :
        IRequest<PagedResult<JobApplicationResponse>>
    {
        public ApplicationFilterRequest Filter { get; set; }

        public GetAllApplicationsQuery(ApplicationFilterRequest filter)
        {
            Filter = filter;
        }
    }
}