using JobApplication.Domain.Enums;

namespace JobApplication.Application.DTOs.Application
{
    public class UpdateAppStatusRequest
    {
        public JobApplicationStatus Status { get; set; }
    }
}