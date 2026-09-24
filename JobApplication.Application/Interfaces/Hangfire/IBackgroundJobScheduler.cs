using System.Linq.Expressions;

namespace JobApplication.Application.Interfaces.Hangfire
{
    public interface IBackgroundJobScheduler
    {
        void Enqueue<T>(Expression<Action<T>> methodCall);
    }
}