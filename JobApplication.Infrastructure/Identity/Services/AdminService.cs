using JobApplication.Application.DTOs.Admin;
using JobApplication.Application.Interfaces;
using JobApplication.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace JobApplication.Infrastructure.Identity.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task CreateRecruiterAsync( CreateRecruiterRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
                throw new Exception("Email is already registered.");

            var recruiter = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(recruiter, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join( ", ",result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            await _userManager.AddToRoleAsync( recruiter, ApplicationRoles.Recruiter);
        }
    }
}