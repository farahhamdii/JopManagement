using JobApplication.Application.DTOs.Application;
using JobApplication.Application.DTOs.Common;
using JobApplication.Application.Interfaces.Repositories;
using MediatR;

namespace JobApplication.Application.Features.Applications.Queries.GetAllApplications
{
    public class GetAllApplicationsHandler :
        IRequestHandler<GetAllApplicationsQuery, PagedResult<JobApplicationResponse>>
    {
        private readonly IApplicationRepository _applicationRepository;

        public GetAllApplicationsHandler(
            IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<PagedResult<JobApplicationResponse>> Handle(GetAllApplicationsQuery request,CancellationToken cancellationToken)
        {
            var result = await _applicationRepository.GetAllAsync(request.Filter);

            var applications = result.Applications.Select(application =>
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

            var totalPages = (int)Math.Ceiling((double)result.TotalCount / request.Filter.PageSize);

            return new PagedResult<JobApplicationResponse>
            {
                Items = applications,
                PageNumber = request.Filter.PageNumber,
                PageSize = request.Filter.PageSize,
                TotalCount = result.TotalCount,
                TotalPages = totalPages
            };
        }
    }
}