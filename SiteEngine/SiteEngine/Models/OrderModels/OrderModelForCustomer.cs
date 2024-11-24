using Models;

namespace SiteEngine.Models.OrderModels
{
    public class OrderModelForCustomer
    {
        public int Orderid { get; set; }
        public string OrderTitleName { get; set; }
        public string OrderAdress { get; set; }
        public string? OrderDescription { get; set; }
        public int OrderDayToDelay { get; set; }
        public string OrderContactInformation { get; set; }
        public UserProfile CreatedUserProfile { get; set; }
        public UserProfile? UserAssignerUserProfile { get; set;}
        public string OrderStatus { get; set; } 
        public string OrderPriority { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
