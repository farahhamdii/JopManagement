using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JobApplication.Application.AppDI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            return services;
        }
    }
}