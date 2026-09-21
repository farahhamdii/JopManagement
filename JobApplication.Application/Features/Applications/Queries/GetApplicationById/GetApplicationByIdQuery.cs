using JobApplication.Application.DTOs.Application;
using MediatR;

namespace JobApplication.Application.Features.Applications.Queries.GetApplicationById
{
    public class GetApplicationByIdQuery : IRequest<JobApplicationResponse>
    {
        public int Id { get; set; }

        public GetApplicationByIdQuery(int id)
        {
            Id = id;
        }
    }
}