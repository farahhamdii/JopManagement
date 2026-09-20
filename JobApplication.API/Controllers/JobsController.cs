using JobApplication.Application.DTOs.Job;
using JobApplication.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Recruiter")]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _jobService;
        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob( [FromBody] CreateJobRequest request)
        {
            var recruiterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _jobService.CreateJobAsync(request,recruiterId!);
            return Ok(new
            {
                message = "Job created successfully."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelJob(int id)
        {
            var recruiterId =User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _jobService.CancelJobAsync(id,recruiterId!);
            return Ok(new
            {
                message = "Job cancelled successfully."
            });
        }
    }
}