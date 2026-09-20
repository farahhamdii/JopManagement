using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JobApplication.Application.DTOs.Admin;

namespace JobApplication.Application.Interfaces
{
    public interface IAdminService
    {
        Task CreateRecruiterAsync(CreateRecruiterRequest request);
    }
}