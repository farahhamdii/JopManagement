using JobApplication.Application.DTOs.Application;
using MediatR;

namespace JobApplication.Application.Features.Applications.Commands.ApplyJob
{
    public class ApplyJobCommand : IRequest
    {
        public ApplyJobRequest Request { get; set; }
        public string UserId { get; set; }

        public ApplyJobCommand(
            ApplyJobRequest request,
            string userId)
        {
            Request = request;
            UserId = userId;
        }
    }
}