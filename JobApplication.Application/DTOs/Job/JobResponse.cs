using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.DTOs.Job
{
    public class JobResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RecruiterId { get; set; }
        public bool IsActive { get; set; }
    }
}