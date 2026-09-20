using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;
using JobApplication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ApplicationDbContext _context;
        public CandidateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();
        }
        public async Task<Candidate?> GetByUserIdAsync(string userId)
        {
            return await _context.Candidates
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}