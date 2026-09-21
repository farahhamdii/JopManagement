using JobApplication.Application.DTOs.Application;
using JobApplication.Application.Interfaces.Repositories;
using MediatR;

namespace JobApplication.Application.Features.Applications.Queries.GetAllApplications
{
    public class GetAllApplicationsQueryHandler
        : IRequestHandler<
            GetAllApplicationsQuery,
            IEnumerable<JobApplicationResponse>>
    {
        private readonly IApplicationRepository _applicationRepository;

        public GetAllApplicationsQueryHandler( IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<IEnumerable<JobApplicationResponse>> Handle(
            GetAllApplicationsQuery request,
            CancellationToken cancellationToken)
        {
            var applications =
                await _applicationRepository.GetAllAsync();

            return applications.Select(application =>
                new JobApplicationResponse
                {
                    Id = application.Id,
                    CandidateId = application.CandidateId,
                    JobId = application.JobId,
                    JobApplicationStatus =
                        application.JobApplicationStatus.ToString(),
                    AppliedAt = application.AppliedAt,
                    StatusUpdatedAt = application.StatusUpdatedAt,
                    CancelledAt = application.CancelledAt
                });
        }
    }
}