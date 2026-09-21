using MediatR;
using JobApplication.Application.DTOs.Job;

namespace JobApplication.Application.Features.Jobs.Queries.GetJobById
{
    public class GetJobByIdQuery : IRequest<JobResponse>
    {
        public int Id { get; set; }

        public GetJobByIdQuery(int id)
        {
            Id = id;
        }
    }
}