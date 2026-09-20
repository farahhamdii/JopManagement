using JobApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobApplication.Infrastructure.Persistence.Configurations
{
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.UserId)
                .IsRequired();

            builder.Property(c => c.CvUrl)
                .HasMaxLength(500)
            .IsRequired(false);

            builder.HasIndex(c => c.UserId)
                .IsUnique();
        }
    }
}