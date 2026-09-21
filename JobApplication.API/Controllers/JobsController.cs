using JobApplication.Application.DTOs.Job;
using JobApplication.Application.Features.Jobs.Commands.CancelJob;
using JobApplication.Application.Features.Jobs.Commands.CreateJob;
using JobApplication.Application.Features.Jobs.Queries.GetAllJobs;
using JobApplication.Application.Features.Jobs.Queries.GetJobById;
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
        public async Task<ActionResult<IEnumerable<JobResponse>>> GetAll()
        {
            var query = new GetAllJobsQuery();
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
    }
}