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
    }
}
