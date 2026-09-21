using MediatR;

namespace JobApplication.Application.Features.Jobs.Commands.CancelJob
{
    public class CancelJobCommand : IRequest
    {
        public int JobId { get; set; }
        public string RecruiterId { get; set; }

        public CancelJobCommand(int jobId, string recruiterId)
        {
            JobId = jobId;
            RecruiterId = recruiterId;
        }
    }
}