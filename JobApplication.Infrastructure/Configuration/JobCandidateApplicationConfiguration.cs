using JobApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobApplication.Infrastructure.Persistence.Configurations
{
    public class JobCandidateApplicationConfiguration
        : IEntityTypeConfiguration<JobCandidateApplication>
    {
        public void Configure(
            EntityTypeBuilder<JobCandidateApplication> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.JobApplicationStatus)
                .IsRequired();

            builder.Property(a => a.AppliedAt)
                .IsRequired();

            builder.Property(a => a.StatusUpdatedAt)
                .IsRequired();

            builder.Property(a => a.CancelledAt)
                .IsRequired(false);

            builder.HasOne(a => a.Candidate)
                .WithMany(c => c.Applications)
                .HasForeignKey(a => a.CandidateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Job)
                .WithMany(j => j.Applications)
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(a => new { a.CandidateId, a.JobId })
                .IsUnique();
        }
    }
}