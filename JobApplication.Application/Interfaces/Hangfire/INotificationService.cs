using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Interfaces.Hangfire
{
    public interface INotificationService
    {
        Task NotifyRecruiterAsync(int applicationId);
    }
}