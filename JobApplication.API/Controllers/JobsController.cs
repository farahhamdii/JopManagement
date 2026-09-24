using JobApplication.Application.DTOs.Common;
using JobApplication.Application.DTOs.Job;
using JobApplication.Application.Features.Jobs.Commands.CancelJob;
using JobApplication.Application.Features.Jobs.Commands.CreateJob;
using JobApplication.Application.Features.Jobs.Commands.UpdateJob;
using JobApplication.Application.Features.Jobs.Queries.GetAllJobs;
using JobApplication.Application.Features.Jobs.Queries.GetJobById;
using JobApplication.Application.Features.Jobs.Queries.GetMyJobs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public JobsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<JobResponse>> GetById(int id)
        {
            var query = new GetJobByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        [Authorize]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PagedResult<JobResponse>>> GetAll([FromQuery] JobFilterRequest filter)
        {
            var query = new GetAllJobsQuery(filter);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> CreateJob([FromBody] CreateJobRequest request)
        {
            var recruiterId =User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new CreateJobCommand(request,recruiterId!);
            var jobId = await _mediator.Send(command);

            return Ok(new
            {
                message = "Job created successfully.",
                jobId = jobId
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> CancelJob(int id)
        {
            var recruiterId =User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new CancelJobCommand( id,recruiterId!);
            await _mediator.Send(command);
            return Ok(new
            {
                message = "Job cancelled successfully."
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] UpdateJobRequest request)
        {
            var recruiterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new UpdateJobCommand(id, request,recruiterId!);
            await _mediator.Send(command);

            return Ok(new
            {
                message = "Job updated successfully."
            });
        }

        [HttpGet("my")]
        [Authorize(Roles = "Recruiter")]
        public async Task<ActionResult<IEnumerable<JobResponse>>> GetMyJobs()
        {
            var recruiterId =User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new GetMyJobsQuery(recruiterId!);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}