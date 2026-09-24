using JobApplication.Application.DTOs.Job;
using MediatR;

namespace JobApplication.Application.Features.Jobs.Commands.UpdateJob
{
    public class UpdateJobCommand : IRequest
    {
        public int JobId { get; set; }
        public UpdateJobRequest Request { get; set; }
        public string RecruiterId { get; set; }

        public UpdateJobCommand(int jobId, UpdateJobRequest request,string recruiterId)
        {
            JobId = jobId;
            Request = request;
            RecruiterId = recruiterId;
        }
    }
}