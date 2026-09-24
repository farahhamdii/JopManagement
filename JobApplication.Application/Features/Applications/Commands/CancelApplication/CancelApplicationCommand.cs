using MediatR;

namespace JobApplication.Application.Features.Applications.Commands.CancelApplication
{
    public class CancelApplicationCommand : IRequest
    {
        public int ApplicationId { get; set; }
        public string UserId { get; set; }
        public CancelApplicationCommand(int applicationId,string userId)
        {
            ApplicationId = applicationId;
            UserId = userId;
        }
    }
}