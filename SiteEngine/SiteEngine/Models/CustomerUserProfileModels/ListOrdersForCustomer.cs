using Models;

namespace SiteEngine.Models.CustomerUserProfileModels
{
    public class ListOrdersForCustomer
    {
        public List<Order?> ListCustomerOrders { get; set; } = new List<Order?>();
        public UserProfile UserProfile { get; set; } = null!;
        public CustomerProfile CustomerProfile { get; set; } = null!;
        public List<OrderPriority> ListCustomerOrderPriorities { get; set; } = null!;
        public List<OrderStatus> ListCustomerOrderStatuses { get; set; } = null!;
        public bool HasError { get; set; } = false;
        public string? ErrorMessage { get; set; } = null;
    }
}
