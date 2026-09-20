using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JobApplication.Domain.Entities;

namespace JobApplication.Application.Interfaces.Repositories
{
    public interface IJobRepository
    {
        Task AddAsync(Job job);
        Task<Job?> GetByIdAsync(int id);
        Task UpdateAsync(Job job); //for change deleteStat to true
    }
}