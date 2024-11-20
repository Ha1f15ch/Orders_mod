using DtoModelsProj;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.InterfaceRepositories
{
    public interface IOrderRepository
    {
        public Task<ReturnCreatedDtoOrderModel> CreateNewOrder(CreateOrderDto newOrderModel);
        public Task<Order?> GetOrderById(int orderId);
        public Task<List<Order?>> GetListOrdersByTitleName(string partialTitleName);
        public Task<List<Order>> GetAllOrders();
    }
}
