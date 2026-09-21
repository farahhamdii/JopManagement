using JobApplication.Application.DTOs.Application;
using MediatR;

namespace JobApplication.Application.Features.Applications.Queries.GetAllApplications
{
    public class GetAllApplicationsQuery
        : IRequest<IEnumerable<JobApplicationResponse>>
    {
    }
}