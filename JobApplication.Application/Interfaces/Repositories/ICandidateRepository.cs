using JobApplication.Domain.Entities;

namespace JobApplication.Application.Interfaces.Repositories
{
    public interface ICandidateRepository
    {
        Task AddAsync(Candidate candidate);
        Task<Candidate?> GetByUserIdAsync(string userId);
    }
}