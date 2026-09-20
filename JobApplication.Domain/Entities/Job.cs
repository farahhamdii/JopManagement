using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Domain.Entities
{
    public class Job
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string RecruiterId { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public DateTime? DeletedAt { get; set; }

        public ICollection<JobCandidateApplication> Applications { get; set; }
            = new List<JobCandidateApplication>();
    }
}