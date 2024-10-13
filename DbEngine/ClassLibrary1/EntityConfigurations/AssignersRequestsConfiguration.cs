using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DatabaseContext.EntityConfigurations
{
    public class AssignersRequestsConfiguration : IEntityTypeConfiguration<AssignersRequests>
    {
        public void Configure(EntityTypeBuilder<AssignersRequests> builder)
        {
            builder.HasOne(ar => ar.User)
                   .WithMany(u => u.AssignersRequests)
                   .HasForeignKey(ar => ar.UserId)
                   .HasPrincipalKey(u => u.Id)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ar => ar.Order)
                   .WithMany(o => o.AssignersRequests)
                   .HasForeignKey(ar => ar.OrderId)
                   .HasPrincipalKey(o => o.Id)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(y => y.DateCreated).HasConversion(
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
