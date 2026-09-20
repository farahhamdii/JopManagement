using JobApplication.Application.DTOs.Admin;
using JobApplication.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("recruiters")]
        public async Task<IActionResult> CreateRecruiter([FromBody] CreateRecruiterRequest request)
        {
            await _adminService.CreateRecruiterAsync(request);
            return Ok(new
            {
                message = "Recruiter account created successfully."
            });
        }
    }
}