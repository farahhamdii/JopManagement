using Hangfire;
using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Hangfire;
using System.Linq.Expressions;

namespace JobApplication.Infrastructure.Services.Hangfire
{
    public class HangfireBackgroundJobScheduler : IBackgroundJobScheduler
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        public HangfireBackgroundJobScheduler(
            IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        public void Enqueue<T>(Expression<Action<T>> methodCall)
        {
            _backgroundJobClient.Enqueue(methodCall);
        }
    }
}