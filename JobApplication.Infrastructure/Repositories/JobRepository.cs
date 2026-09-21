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

        public async Task<Job?> GetByIdAsync(int id)
        {
            return await _context.Jobs.FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task UpdateAsync(Job job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            return await _context.Jobs
                .Where(j => !j.IsDeleted && j.IsActive)
                .ToListAsync();
        }
    }
}