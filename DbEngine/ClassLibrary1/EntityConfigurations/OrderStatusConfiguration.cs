using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DatabaseContext.EntityConfigurations
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasData(
                new OrderStatus
                {
                    OrderStatusId = "N",
                    Description = "Новый"
                },
                new OrderStatus
                {
                    OrderStatusId = "D",
                    Description = "Утверждение иполнителя"
                },
                new OrderStatus
                {
                    OrderStatusId = "S",
                    Description = "Начат"
                },
                new OrderStatus
                {
                    OrderStatusId = "C",
                    Description = "Отменен"
                },
                new OrderStatus
                {
                    OrderStatusId = "X",
                    Description = "Удален"
                },
                new OrderStatus
                {
                    OrderStatusId = "F",
                    Description = "Завершен"
                }
            );
        }
    }
}
