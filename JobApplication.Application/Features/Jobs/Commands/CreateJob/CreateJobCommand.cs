using JobApplication.Application.DTOs.Job;
using MediatR;

namespace JobApplication.Application.Features.Jobs.Commands.CreateJob
{
    public class CreateJobCommand : IRequest<int>
    {
        public CreateJobRequest Request { get; set; }
        public string RecruiterId { get; set; }
        public CreateJobCommand(CreateJobRequest request, string recruiterId)
        {
            Request = request;
            RecruiterId = recruiterId;
        }
    }
}