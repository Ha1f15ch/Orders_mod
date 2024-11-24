using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.InterfaceRepositories
{
    public interface IOrderStatusRepository
    {
        public Task<List<OrderStatus>> GetAllOrderStatuses();
        public Task<OrderStatus?> GetOrderStatusById(string orderStatusId);
    }
}
