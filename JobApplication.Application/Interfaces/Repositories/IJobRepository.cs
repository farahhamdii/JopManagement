using JobApplication.Application.DTOs.Job;
using JobApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Interfaces.Repositories
{
    public interface IJobRepository
    {
        Task AddAsync(Job job);
        Task<Job?> GetByIdAsync(int id);
        Task UpdateAsync(Job job); //for change deleteStat to true
        Task<(IEnumerable<Job> Jobs, int TotalCount)> GetAllAsync(JobFilterRequest filter);
        Task<IEnumerable<Job>> GetByRecruiterIdAsync(string recruiterId);
    }
}