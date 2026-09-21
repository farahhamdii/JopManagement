using JobApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Interfaces.Repositories
{
    public interface IApplicationRepository
    {
        Task AddAsync(JobCandidateApplication application);
        Task<IEnumerable<JobCandidateApplication>> GetAllAsync();
        Task<JobCandidateApplication?> GetByIdAsync(int id);
        Task<JobCandidateApplication?> GetByCandidateAndJobAsync(int candidateId, int jobId);
        Task UpdateAsync(JobCandidateApplication application);
        Task<IEnumerable<JobCandidateApplication>> GetByCandidateIdAsync(int candidateId);
    }
}
