using JobApplication.Application.DTOs.Application;
using JobApplication.Application.DTOs.Common;
using JobApplication.Application.Features.Applications.Commands.ApplyJob;
using JobApplication.Application.Features.Applications.Commands.CancelApplication;
using JobApplication.Application.Features.Applications.Commands.UpdateApp;
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
<<<<<<< HEAD
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new ApplyJobCommand( request,userId!);
=======
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new ApplyJobCommand( request, userId!);
>>>>>>> 16b04616e9c4bce2f9fcfd5fa4ef8f1135bb4574
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
<<<<<<< HEAD
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new CancelApplicationCommand(id,userId!);
=======
            var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new CancelApplicationCommand( id, userId!);
>>>>>>> 16b04616e9c4bce2f9fcfd5fa4ef8f1135bb4574
            await _mediator.Send(command);
            return Ok(new
            {
                message = "Application cancelled successfully."
            });
        }
        [HttpPut("{id}/status")]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> UpdateStatus(int id,[FromBody] UpdateAppStatusRequest request)
        {
            var recruiterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new UpdateAppStatusCommand( id,request,recruiterId!);
            await _mediator.Send(command);
            return Ok(new
            {
                message = "Application status updated successfully."
            });
        }

        [HttpGet]
        [Authorize(Roles = "Recruiter,Admin")]
        public async Task<ActionResult<PagedResult<JobApplicationResponse>>> GetAll(
            [FromQuery] ApplicationFilterRequest filter)
        {
<<<<<<< HEAD
            var query = new GetAllApplicationsQuery(filter);
=======
            var query = new GetAllApplicationsQuery();
>>>>>>> 16b04616e9c4bce2f9fcfd5fa4ef8f1135bb4574
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Candidate,Recruiter,Admin")]
        public async Task<ActionResult<JobApplicationResponse>> GetById(int id)
        {
            var query = new GetApplicationByIdQuery(id);
<<<<<<< HEAD
            var result = await _mediator.Send(query);
=======
            var result = await _mediator.Send(query); //track
>>>>>>> 16b04616e9c4bce2f9fcfd5fa4ef8f1135bb4574
            return Ok(result);
        }

        [HttpGet("my")]
        [Authorize(Roles = "Candidate")]
        public async Task<ActionResult<IEnumerable<JobApplicationResponse>>> GetMyApplications()
        {
<<<<<<< HEAD
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
=======
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);
>>>>>>> 16b04616e9c4bce2f9fcfd5fa4ef8f1135bb4574
            var query = new GetMyApplicationsQuery(userId!);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
