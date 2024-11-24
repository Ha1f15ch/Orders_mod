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
    public class OrderPriorityRepository : IOrderPriorityRepository
    {
        private readonly AppDbContext context;

        public OrderPriorityRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<OrderPriority>> GetAllPriority()
        {
            try
            {
                return await context.OrderPriorities.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"При получении списка приоритетов для заказов возникла ошибка - {ex.Message}");
                return new List<OrderPriority>();
            }
        }

        public async Task<OrderPriority?> GetorderPriorityById(string orderPriorityId)
        {
            try
            {
                if (!string.IsNullOrEmpty(orderPriorityId))
                {
                    return await context.OrderPriorities.FindAsync(orderPriorityId);
                }

                Console.WriteLine($"Заданное значение для поиска - {orderPriorityId} - некорректно");
                return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"При поиске приоритета для заказа по id = {orderPriorityId}, возникла ошибка - {ex.Message}");
                return null;
            }
        }
    }
}
