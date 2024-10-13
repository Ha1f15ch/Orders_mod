using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DatabaseContext.EntityConfigurations
{
    public class OrderScoresConfiguration : IEntityTypeConfiguration<OrderScores>
    {
        public void Configure(EntityTypeBuilder<OrderScores> builder)
        {
            builder.HasOne(os => os.User)
                   .WithMany(u => u.OrderScores)
                   .HasForeignKey(os => os.UserId)
                   .HasPrincipalKey(u => u.Id)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(os => os.Order)
                   .WithMany(o => o.OrderScores)
                   .HasForeignKey(os => os.OrderId)
                   .HasPrincipalKey(o => o.Id)
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
