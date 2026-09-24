using JobApplication.Application.DTOs.Application;
using JobApplication.Application.Interfaces.Repositories;
using MediatR;

namespace JobApplication.Application.Features.Applications.Queries.GetMyApplications
{
    public class GetMyApplicationsQueryHandler: IRequestHandler<GetMyApplicationsQuery, IEnumerable<JobApplicationResponse>>
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ICandidateRepository _candidateRepository;
        public GetMyApplicationsQueryHandler(
            IApplicationRepository applicationRepository,
            ICandidateRepository candidateRepository)
        {
            _applicationRepository = applicationRepository;
            _candidateRepository = candidateRepository;
        }

        public async Task<IEnumerable<JobApplicationResponse>> Handle(GetMyApplicationsQuery request,
            CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByUserIdAsync(request.UserId);

            if (candidate == null) throw new Exception("Candidate not found.");

            var applications = await _applicationRepository .GetByCandidateIdAsync(candidate.Id);

            return applications.Select(application => new JobApplicationResponse
            {
                Id = application.Id,
                CandidateId = application.CandidateId,
                JobId = application.JobId,
                JobApplicationStatus = application.JobApplicationStatus.ToString(),
                AppliedAt = application.AppliedAt,
                StatusUpdatedAt = application.StatusUpdatedAt,
                CancelledAt = application.CancelledAt
            });
        }
    }
}