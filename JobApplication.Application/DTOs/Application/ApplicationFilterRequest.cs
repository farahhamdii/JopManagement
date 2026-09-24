using JobApplication.Domain.Enums;

namespace JobApplication.Application.DTOs.Application
{
    public class ApplicationFilterRequest
    {
        public JobApplicationStatus? Status { get; set; }
        public int? JobId { get; set; }
        public int? CandidateId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}