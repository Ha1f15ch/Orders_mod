using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DatabaseContext.EntityConfigurations
{
    public class RequestsToCancellationConfiguration : IEntityTypeConfiguration<RequestsToCancellation>
    {
        public void Configure(EntityTypeBuilder<RequestsToCancellation> builder)
        {
            builder.HasOne(rtc => rtc.Initiator)
                   .WithMany(u => u.RequestsToCancellationsInitiator)
                   .HasForeignKey(rtc => rtc.InitiatorUserId)
                   .HasPrincipalKey(u => u.Id)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(rtc => rtc.ConfirmedUser)
                   .WithMany(u => u.RequestsToCancellationsConfirmer)
                   .HasForeignKey(rtc => rtc.ConfirmedUserId)
                   .HasPrincipalKey(u => u.Id)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(rtc => rtc.Order)
                   .WithMany(o => o.RequestsToCancellations)
                   .HasForeignKey(rtc => rtc.OrderId)
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
