using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;
using JobApplication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Infrastructure.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(JobCandidateApplication application)
        {
            await _context.JobCandidateApplications.AddAsync(application);
            await _context.SaveChangesAsync();
        }

        public async Task<JobCandidateApplication?> GetByIdAsync(int id)
        {
            return await _context.JobCandidateApplications
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<JobCandidateApplication?> GetByCandidateAndJobAsync(int candidateId, int jobId)
        {
            return await _context.JobCandidateApplications
                .FirstOrDefaultAsync(a => a.CandidateId == candidateId &&a.JobId == jobId);
        }

        public async Task UpdateAsync(JobCandidateApplication application)
        {
            _context.JobCandidateApplications.Update(application);
            await _context.SaveChangesAsync();
        }
    }
}