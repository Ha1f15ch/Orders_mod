using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.InterfaceRepositories
{
    public interface IOrderPriorityRepository
    {
        public Task<List<OrderPriority>> GetAllPriority();
    }
}
