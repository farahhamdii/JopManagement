using JobApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobApplication.Infrastructure.Persistence.Configurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(j => j.Id);

            builder.Property(j => j.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(j => j.Description)
                .IsRequired();

            builder.Property(j => j.RecruiterId)
                .IsRequired();

            builder.Property(j => j.IsActive)
                .HasDefaultValue(true);

            builder.Property(j => j.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}