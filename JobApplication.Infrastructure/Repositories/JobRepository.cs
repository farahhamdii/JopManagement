using JobApplication.Application.DTOs.Job;
using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;
using JobApplication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Infrastructure.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDbContext _context;
        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Job job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
        }
        public async Task<Job?> GetByIdAsync(int id)
        {
            return await _context.Jobs.FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<(IEnumerable<Job> Jobs, int TotalCount)> GetAllAsync( JobFilterRequest filter)
        {
            var query = _context.Jobs.Where(j => !j.IsDeleted).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                query = query.Where(j => j.Title.Contains(filter.Keyword) || j.Description.Contains(filter.Keyword));
            }

            if (filter.IsActive.HasValue)
            {
                query = query.Where(j =>j.IsActive == filter.IsActive.Value);
            }

            var totalCount = await query.CountAsync();

            var jobs = await query.Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return (jobs, totalCount);
        }
        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            return await _context.Jobs.Where(j => !j.IsDeleted && j.IsActive)
                .ToListAsync();
        }
        public async Task<IEnumerable<Job>> GetByRecruiterIdAsync(string recruiterId)
        {
            return await _context.Jobs.Where(j => j.RecruiterId == recruiterId && !j.IsDeleted)
                .ToListAsync();
        }
    }
}