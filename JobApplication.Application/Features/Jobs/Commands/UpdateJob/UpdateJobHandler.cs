using JobApplication.Application.Interfaces.Repositories;
using MediatR;

namespace JobApplication.Application.Features.Jobs.Commands.UpdateJob
{
    public class UpdateJobHandler : IRequestHandler<UpdateJobCommand>
    {
        private readonly IJobRepository _jobRepository;
        public UpdateJobHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task Handle(UpdateJobCommand request,
            CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);

            if (job == null) throw new Exception("Job not found.");

            if (job.RecruiterId != request.RecruiterId)
                throw new Exception("You are not allowed to update this job.");

            if (job.IsDeleted || !job.IsActive)
                throw new Exception("You cannot update this job.");

            job.Title = request.Request.Title;
            job.Description = request.Request.Description;

            await _jobRepository.UpdateAsync(job);
        }
    }
}