using DatabaseContext;
using DtoModelsProj;
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
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;

        public OrderRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Task<ReturnCreatedDtoOrderModel> CreateNewOrder(CreateOrderDto newOrderModel)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await context.Orders.ToListAsync();
        }

        public async Task<List<Order>> GetListOrdersByTitleName(string partialTitleName)
        {
            try
            {
                if (!string.IsNullOrEmpty(partialTitleName))
                {
                    return await context.Orders.Where(el => el.TitleName.Contains(partialTitleName)).ToListAsync();
                }

                return new List<Order>();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Hе удалось найти заказ по partialTitleName = {partialTitleName} \nВозникла ошибка - {ex.Message}");
                return new List<Order>();
            }
        }

        public async Task<Order?> GetOrderById(int orderId)
        {
            try
            {
                if(orderId > 0) return await context.Orders.FindAsync(orderId);

                Console.WriteLine($"Не удалоось найти запись заказа по id, передано некорректное знаение orderId = {nameof(orderId)}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникла ошибка при поиске заказа с id = {orderId}. Error = {ex.Message}");
                return null;
            }
        }
    }
}
