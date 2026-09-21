using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;
using MediatR;

namespace JobApplication.Application.Features.Jobs.Commands.CreateJob
{
    public class CreateJobHandler: IRequestHandler<CreateJobCommand, int>
    {
        private readonly IJobRepository _jobRepository;

        public CreateJobHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<int> Handle(
            CreateJobCommand request,
            CancellationToken cancellationToken)
        {
            var job = new Job
            {
                Title = request.Request.Title,
                Description = request.Request.Description,
                RecruiterId = request.RecruiterId,
                IsActive = true,
                IsDeleted = false
            };

            await _jobRepository.AddAsync(job);

            return job.Id;
        }
    }
}