using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoModelsProj
{
    public class ListOrdersForCustomerDto
    {
        public List<Order?> ListOrders { get; set; } = new List<Order?>();
        public UserProfile UserProfile { get; set; } = null!;
        public CustomerProfile CustomerProfile { get; set; } = null!;
        public List<OrderPriority> ListOrderPriorities { get; set; } = null!;
        public List<OrderStatus> ListOrderStatuses { get; set; } = null!;
        public bool HasError { get; set; } = false;
        public string? ErrorMessage { get; set; } = null;
    }
}
