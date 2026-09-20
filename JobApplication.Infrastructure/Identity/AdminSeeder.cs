using Microsoft.AspNetCore.Identity;

namespace JobApplication.Infrastructure.Identity
{
    public static class AdminSeeder
    {
        public static async Task SeedAdminAsync( UserManager<ApplicationUser> userManager)
        {
            var adminEmail = "admin@jobapplication.com";
            var adminPassword = "Admin123$";
            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
            if (existingAdmin != null)
                return;
            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync( admin,adminPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            await userManager.AddToRoleAsync( admin,ApplicationRoles.Admin);
        }
    }
}