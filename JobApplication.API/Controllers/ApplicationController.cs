using JobApplication.Application.DTOs.Application;
using JobApplication.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Candidate")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        public ApplicationController(
            IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        public async Task<IActionResult> Apply(
            [FromBody] ApplyJobRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _applicationService.ApplyAsync(request,userId!);
            return Ok(new
            {
                message = "Application submitted successfully."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _applicationService.CancelAsync( id, userId!);
            return Ok(new
            {
                message = "Application cancelled successfully."
            });
        }
    }
}