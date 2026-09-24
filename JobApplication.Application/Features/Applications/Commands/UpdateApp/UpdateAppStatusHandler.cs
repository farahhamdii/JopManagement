using JobApplication.Application.Interfaces.Repositories;
using MediatR;

namespace JobApplication.Application.Features.Applications.Commands.UpdateApp
{
    public class UpdateAppStatusHandler :
        IRequestHandler<UpdateAppStatusCommand>
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IJobRepository _jobRepository;

        public UpdateAppStatusHandler(
            IApplicationRepository applicationRepository,
            IJobRepository jobRepository)
        {
            _applicationRepository = applicationRepository;
            _jobRepository = jobRepository;
        }

        public async Task Handle(UpdateAppStatusCommand request,CancellationToken cancellationToken)
        {
            var application = await _applicationRepository.GetByIdAsync(request.ApplicationId);
            if (application == null)
                throw new Exception("Application not found.");
            var job = await _jobRepository .GetByIdAsync(application.JobId);
            if (job == null)
                throw new Exception("Job not found.");

            if (job.RecruiterId != request.RecruiterId)
                throw new Exception( "You are not allowed to update this application.");

            if (application.JobApplicationStatus ==
                JobApplication.Domain.Enums.JobApplicationStatus.Cancelled)
                throw new Exception("You cannot update a cancelled application.");

            application.JobApplicationStatus = request.Request.Status;
            application.StatusUpdatedAt = DateTime.UtcNow;
            await _applicationRepository.UpdateAsync(application);
        }
    }
}