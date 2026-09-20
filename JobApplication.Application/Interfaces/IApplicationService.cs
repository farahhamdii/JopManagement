using JobApplication.Application.DTOs.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Interfaces
{
    public interface IApplicationService
    {
        Task ApplyAsync( ApplyJobRequest request, string userId);
        Task CancelAsync( int applicationId,string userId);
    }
}
