using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DatabaseContext.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(o => o.UserCreator)
                   .WithMany(u => u.OrdersCreated)
                   .HasForeignKey(o => o.UserIdCreated)
                   .HasPrincipalKey(u => u.Id)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.UserAssigner)
                   .WithMany(u => u.OrdersAssigned)
                   .HasForeignKey(o => o.UserIdAssigner)
                   .HasPrincipalKey(u => u.Id)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.OrderStatus)
                   .WithMany(os => os.Orders)
                   .HasForeignKey(o => o.OrderStatusId)
                   .HasPrincipalKey(os => os.OrderStatusId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.OrderPriority)
                   .WithMany(op => op.Orders)
                   .HasForeignKey(o => o.OrderPriorityId)
                   .HasPrincipalKey(op => op.OrderPriorityId)
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