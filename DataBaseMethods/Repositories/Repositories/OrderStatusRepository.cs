using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly AppDbContext context;

        public OrderStatusRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<OrderStatus>> GetAllOrderStatuses()
        {
            try
            {
                return await context.OrderStatuses.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"При возвращении списка статусов заказов возникла ошибка - {ex.Message}");
                return new List<OrderStatus>();
            }
        }

        public async Task<OrderStatus?> GetOrderStatusById(string orderStatusId)
        {
            try
            {
                return await context.OrderStatuses.FindAsync(orderStatusId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Во время поиска статуса заказа по id возникла ошибка - {ex.Message}");
                return null;
            }
        }
    }
}
