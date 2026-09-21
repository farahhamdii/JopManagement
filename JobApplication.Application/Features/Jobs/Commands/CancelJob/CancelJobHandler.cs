using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Enums;
using MediatR;

namespace JobApplication.Application.Features.Jobs.Commands.CancelJob
{
    public class CancelJobHandler : IRequestHandler<CancelJobCommand>
    {
        private readonly IJobRepository _jobRepository;

        public CancelJobHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task Handle(CancelJobCommand request,
            CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);

            if (job == null)
                throw new Exception("Job not found.");

            if (job.RecruiterId != request.RecruiterId)
                throw new UnauthorizedAccessException(
                    "You can only cancel your own jobs.");

            if (job.IsDeleted)
                throw new Exception("Job is already cancelled.");

            job.IsActive = false;
            job.IsDeleted = true;
            job.DeletedAt = DateTime.UtcNow;

            await _jobRepository.UpdateAsync(job);
        }
    }
}