using JobApplication.Application.Interfaces;
using JobApplication.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace JobApplication.Application.AppDI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IApplicationService, ApplicationService>();

            services.AddMediatR(cfg =>
              cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}