namespace JobApplication.Application.DTOs.Application
{
    public class JobApplicationResponse
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int JobId { get; set; }
        public string JobApplicationStatus { get; set; }
        public DateTime AppliedAt { get; set; }
        public DateTime StatusUpdatedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
    }
}