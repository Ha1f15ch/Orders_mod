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

        public async Task<ReturnCreatedDtoOrderModel> CreateNewOrder(CreateOrderDto newOrderModel)
        {
            try
            {
                var user = await context.Users.FindAsync(newOrderModel.Dto_UserIdCreated);

                if(user != null && !string.IsNullOrEmpty(newOrderModel.Dto_TitleName) && !string.IsNullOrEmpty(newOrderModel.Dto_Adress) && (newOrderModel.Dto_DayToDelay > 0 && newOrderModel.Dto_UserIdCreated > 0) && !string.IsNullOrEmpty(newOrderModel.Dto_ContactInformation) && !string.IsNullOrEmpty(newOrderModel.Dto_OrderPriorityId))
                {
                    var order = new Order
                    {
                        TitleName = newOrderModel.Dto_TitleName,
                        Adress = newOrderModel.Dto_Adress,
                        Description = newOrderModel.Dto_Description,
                        DayToDelay = newOrderModel.Dto_DayToDelay,
                        ContactInformation = newOrderModel.Dto_ContactInformation,
                        UserIdCreated = newOrderModel.Dto_UserIdCreated,
                        UserIdAssigner = newOrderModel.Dto_UserIdAssigner,
                        OrderStatusId = newOrderModel.Dto_OrderStatusId,
                        OrderPriorityId = newOrderModel.Dto_OrderPriorityId,
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow,
                        DateDeleted = null,
                    };

                    await context.AddAsync(order);
                    await context.SaveChangesAsync();

                    var orderId = order.Id;

                    return new ReturnCreatedDtoOrderModel
                    {
                        IsCreated = true,
                        OrderId = orderId,
                    };
                }

                Console.WriteLine($"Невозможно корректно создать запись - Order. Переданы некорректные данные: \nnewOrderModel.Dto_TitleName = {newOrderModel.Dto_TitleName}, \nnewOrderModel.Dto_Adress = {newOrderModel.Dto_Adress} \nnewOrderModel.Dto_DayToDelay = {newOrderModel.Dto_DayToDelay} \nnewOrderModel.Dto_UserIdCreated = {newOrderModel.Dto_UserIdCreated} \nnewOrderModel.Dto_ContactInformation = {newOrderModel.Dto_ContactInformation} \nnewOrderModel.Dto_OrderPriorityId = {newOrderModel.Dto_OrderPriorityId}");
                return new ReturnCreatedDtoOrderModel()
                {
                    IsCreated = false,
                    OrderId = 0
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникла ошибка при создании нового заказа - {ex.Message}");
                return new ReturnCreatedDtoOrderModel()
                {
                    IsCreated = false,
                    OrderId = 0
                };
            }
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
