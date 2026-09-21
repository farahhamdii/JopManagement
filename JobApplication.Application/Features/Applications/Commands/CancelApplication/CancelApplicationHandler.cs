using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Enums;
using MediatR;

namespace JobApplication.Application.Features.Applications.Commands.CancelApplication
{
    public class CancelApplicationHandler
        : IRequestHandler<CancelApplicationCommand>
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ICandidateRepository _candidateRepository;

        public CancelApplicationHandler(
            IApplicationRepository applicationRepository,
            ICandidateRepository candidateRepository)
        {
            _applicationRepository = applicationRepository;
            _candidateRepository = candidateRepository;
        }

        public async Task Handle(
            CancelApplicationCommand request,
            CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository
                .GetByUserIdAsync(request.UserId);

            if (candidate == null)
                throw new Exception("Candidate not found.");

            var application = await _applicationRepository
                .GetByIdAsync(request.ApplicationId);

            if (application == null)
                throw new Exception("Application not found.");

            if (application.CandidateId != candidate.Id)
                throw new UnauthorizedAccessException(
                    "You can only cancel your own applications.");

            if (application.JobApplicationStatus != JobApplicationStatus.Applied)
                throw new Exception(
                    "You can only cancel an application with Applied status.");

            application.JobApplicationStatus =
                JobApplicationStatus.Cancelled;

            application.CancelledAt = DateTime.UtcNow;
            application.StatusUpdatedAt = DateTime.UtcNow;

            await _applicationRepository.UpdateAsync(application);
        }
    }
}