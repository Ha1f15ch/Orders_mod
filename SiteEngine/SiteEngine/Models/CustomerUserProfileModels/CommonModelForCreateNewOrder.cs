using Models;

namespace SiteEngine.Models.CustomerUserProfileModels
{
    public class CommonModelForCreateNewOrder
    {
        public CreateNewOrderModel CreateNewOrderModel { get; set; }
        public List<OrderPriority> OrderPriorities { get; set; }
    }
}
