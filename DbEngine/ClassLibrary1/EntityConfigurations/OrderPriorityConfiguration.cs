using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DatabaseContext.EntityConfigurations
{
    public class OrderPriorityConfiguration : IEntityTypeConfiguration<OrderPriority>
    {
        public void Configure(EntityTypeBuilder<OrderPriority> builder)
        {
            builder.HasData(
                new OrderPriority
                {
                    OrderPriorityId = "L",
                    Description = "Низкий"
                },
                new OrderPriority
                {
                    OrderPriorityId = "M",
                    Description = "Средний"
                },
                new OrderPriority
                {
                    OrderPriorityId = "H",
                    Description = "Высокий"
                }
            );
        }
    }
}
