using JobApplication.Application.DTOs.Application;
using JobApplication.Application.Interfaces.Repositories;
using MediatR;

namespace JobApplication.Application.Features.Applications.Queries.GetApplicationById
{
    public class GetApplicationByIdQueryHandler : IRequestHandler<GetApplicationByIdQuery, JobApplicationResponse>
    {
        private readonly IApplicationRepository _applicationRepository;

        public GetApplicationByIdQueryHandler(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<JobApplicationResponse> Handle( GetApplicationByIdQuery request,CancellationToken cancellationToken)
        {
            var application = await _applicationRepository.GetByIdAsync(request.Id);
            if (application == null)throw new Exception("Application not found.");

            return new JobApplicationResponse
            {
                Id = application.Id,
                CandidateId = application.CandidateId,
                JobId = application.JobId,
                JobApplicationStatus =application.JobApplicationStatus.ToString(),
                AppliedAt = application.AppliedAt,
                StatusUpdatedAt = application.StatusUpdatedAt,
                CancelledAt = application.CancelledAt
            };
        }
    }
}