using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DatabaseContext.EntityConfigurations
{
    public class OrderScoresEmployerConfiguration : IEntityTypeConfiguration<OrderScoresEmployer>
    {
        public void Configure(EntityTypeBuilder<OrderScoresEmployer> builder)
        {
            builder.HasOne(os => os.EmployerProfile)
                   .WithMany(ep => ep.OrderScoresEmployers)
                   .HasForeignKey(os => os.EmployerProfileId)
                   .HasPrincipalKey(u => u.Id)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(y => y.DateCreated).HasConversion(
                t => t.ToUniversalTime(),
                t => DateTime.SpecifyKind(t, DateTimeKind.Utc)
            );
            builder.Property(y => y.DateUpdated).HasConversion(
                t => t.ToUniversalTime(),
                t => DateTime.SpecifyKind(t, DateTimeKind.Utc)
            );
            builder.Property(y => y.DateDeleted).HasConversion(
                t => t.HasValue ? t.Value.ToUniversalTime() : (DateTime?)null,
                t => t.HasValue ? DateTime.SpecifyKind(t.Value, DateTimeKind.Utc) : (DateTime?)null
            );
        }
    }
}
