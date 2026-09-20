using JobApplication.Application.DTOs.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Services
{
    public interface IJobService
    {
        Task CreateJobAsync(CreateJobRequest request, string recruiterId);
        Task CancelJobAsync(int jobId, string recruiterId);
    }
}