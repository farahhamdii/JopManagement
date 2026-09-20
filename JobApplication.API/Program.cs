using JobApplication.Application.AppDI;
using JobApplication.Infrastructure;
using JobApplication.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace JobApplication.API;

public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddApplication();

        builder.Services.AddInfrastructure(
            builder.Configuration);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer",
                new Microsoft.OpenApi.OpenApiSecurityScheme
                {
                    Type = Microsoft.OpenApi.SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Enter your JWT token."
                });

            options.AddSecurityRequirement(document =>
                new Microsoft.OpenApi.OpenApiSecurityRequirement
                {
                    [new Microsoft.OpenApi.OpenApiSecuritySchemeReference(
                        "Bearer",
                        document)] = []
                });
        });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var roleManager =
                scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

            var userManager =
                scope.ServiceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();

            await IdentitySeeder.SeedRolesAsync(roleManager);

            await AdminSeeder.SeedAdminAsync(userManager);
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}