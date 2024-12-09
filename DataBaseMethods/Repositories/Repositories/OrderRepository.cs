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

        public async Task<List<Order?>> GetFilteredListOrders(DateOnly startCreatedDate, DateOnly endCreatedDate, DateOnly startDeletedDate, DateOnly endDeletedDate, string? listStatuses, string? listPriorities, bool isCustomer, int userId)
        {
            try
            {
                CustomerProfile? customer = null;
                EmployerProfile? employer = null;
                User? user = await context.Users.FindAsync(userId);

                if(isCustomer)
                {
                    customer = await context.CustomerProfiles.SingleOrDefaultAsync(el => el.UserId == userId);
                }

                if(!isCustomer)
                {
                    employer = await context.EmployerProfiles.SingleOrDefaultAsync(el => el.UserId == userId);
                }

                IQueryable<Order> selectedOrders = context.Orders;

                if(isCustomer && customer != null && user != null)
                {
                    selectedOrders = selectedOrders.Where(order => order.UserIdCreated == user.Id);
                }
                else if (!isCustomer && employer != null && user !=null)
                {//Исполнитель, имеет смысл добавить ограничение по статусам заказа
                    selectedOrders = selectedOrders.Where(order => order.UserIdAssigner == user.Id && order.UserIdAssigner != order.UserIdCreated);
                }

                if (startCreatedDate > DateOnly.MinValue && endCreatedDate > DateOnly.MinValue && (startCreatedDate <= endCreatedDate))
                {
                    selectedOrders = selectedOrders.Where(order => DateOnly.FromDateTime(order.DateCreated) >= startCreatedDate &&
                                                                   DateOnly.FromDateTime(order.DateCreated) <= endCreatedDate);
                }
                else if (startCreatedDate > DateOnly.MinValue)
                {
                    selectedOrders = selectedOrders.Where(order => DateOnly.FromDateTime(order.DateCreated) >= startCreatedDate);
                }
                else if (endCreatedDate > DateOnly.MinValue)
                {
                    selectedOrders = selectedOrders.Where(order => DateOnly.FromDateTime(order.DateCreated) <= endCreatedDate);
                }

                if (startDeletedDate > DateOnly.MinValue && endDeletedDate > DateOnly.MinValue && (startDeletedDate <= endDeletedDate))
                {
                    selectedOrders = selectedOrders.Where(order => order.DateDeleted.HasValue &&
                                                                   DateOnly.FromDateTime(order.DateDeleted.Value) >= startDeletedDate &&
                                                                   DateOnly.FromDateTime(order.DateDeleted.Value) <= endDeletedDate &&
                                                                   (order.OrderStatusId.Contains("C") || order.OrderStatusId.Contains("X")));
                }
                else if (startDeletedDate > DateOnly.MinValue)
                {
                    selectedOrders = selectedOrders.Where(order => order.DateDeleted.HasValue &&
                                                                   DateOnly.FromDateTime(order.DateDeleted.Value) >= startDeletedDate &&
                                                                   (order.OrderStatusId.Contains("C") || order.OrderStatusId.Contains("X")));
                }
                else if (endDeletedDate > DateOnly.MinValue)
                {
                    selectedOrders = selectedOrders.Where(order => order.DateDeleted.HasValue &&
                                                                   DateOnly.FromDateTime(order.DateDeleted.Value) <= endDeletedDate &&
                                                                   (order.OrderStatusId.Contains("C") || order.OrderStatusId.Contains("X")));
                }

                if (!string.IsNullOrEmpty(listStatuses))
                {
                    var statusIds = listStatuses.Split(',').ToList();
                    selectedOrders = selectedOrders.Where(order => statusIds.Any(id => order.OrderStatusId.Equals(id)));
                }

                if (!string.IsNullOrEmpty(listPriorities))
                {
                    var priorityIds = listPriorities.Split(',').ToList();
                    selectedOrders = selectedOrders.Where(order => priorityIds.Any(id => order.OrderPriorityId.Equals(id)));
                }

                return await selectedOrders.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Возникла ошибка при поиске заказов по фильтрам - {ex.Message}");
                return await context.Orders?.ToListAsync();
            }
        }
    }
}
