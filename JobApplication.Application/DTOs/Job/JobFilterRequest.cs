namespace JobApplication.Application.DTOs.Job
{
    public class JobFilterRequest
    {
        public string? Keyword { get; set; }
        public bool? IsActive { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}