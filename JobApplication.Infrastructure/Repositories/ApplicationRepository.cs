using JobApplication.Application.DTOs.Application;
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
        public async Task<(IEnumerable<JobCandidateApplication> Applications, int TotalCount)>
                GetAllAsync(ApplicationFilterRequest filter)
        {
            var query = _context.JobCandidateApplications
                .AsQueryable();

            if (filter.Status.HasValue)
            {
                query = query.Where(a => a.JobApplicationStatus == filter.Status.Value);
            }

            if (filter.JobId.HasValue)
            {
                query = query.Where(a =>a.JobId == filter.JobId.Value);
            }

            if (filter.CandidateId.HasValue)
            {
                query = query.Where(a =>a.CandidateId == filter.CandidateId.Value);
            }

            var totalCount = await query.CountAsync();

            var applications = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return (applications, totalCount);
        }

        public async Task<JobCandidateApplication?> GetByIdAsync(int id)
        {
            return await _context.JobCandidateApplications.FirstOrDefaultAsync(a => a.Id == id);
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

        public async Task<IEnumerable<JobCandidateApplication>> GetByCandidateIdAsync( int candidateId)
        {
            return await _context.JobCandidateApplications.Where(a => a.CandidateId == candidateId)
                .ToListAsync();
        }
    }
}