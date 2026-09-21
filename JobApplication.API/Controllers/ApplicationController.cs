using JobApplication.Application.DTOs.Application;
using JobApplication.Application.Features.Applications.Commands.ApplyJob;
using JobApplication.Application.Features.Applications.Commands.CancelApplication;
using JobApplication.Application.Features.Applications.Queries.GetAllApplications;
using JobApplication.Application.Features.Applications.Queries.GetApplicationById;
using JobApplication.Application.Features.Applications.Queries.GetMyApplications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ApplicationController : ControllerBase
    {
        private readonly IMediator _mediator;  //for update
        public ApplicationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> Apply(
            [FromBody] ApplyJobRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new ApplyJobCommand( request, userId!);
            await _mediator.Send(command);
            return Ok(new
            {
                message = "Application submitted successfully."
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new CancelApplicationCommand( id, userId!);
            await _mediator.Send(command);
            return Ok(new
            {
                message = "Application cancelled successfully."
            });
        }

        [HttpGet]
        [Authorize(Roles = "Recruiter,Admin")]
        public async Task<ActionResult<IEnumerable<JobApplicationResponse>>> GetAll()
        {
            var query = new GetAllApplicationsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Candidate,Recruiter,Admin")]
        public async Task<ActionResult<JobApplicationResponse>> GetById(int id)
        {
            var query = new GetApplicationByIdQuery(id);
            var result = await _mediator.Send(query); //track
            return Ok(result);
        }

        [HttpGet("my")]
        [Authorize(Roles = "Candidate")]
        public async Task<ActionResult<IEnumerable<JobApplicationResponse>>> GetMyApplications()
        {
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new GetMyApplicationsQuery(userId!);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
