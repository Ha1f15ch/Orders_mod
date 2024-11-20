using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Order", Schema = "dbo")]
    public class Order
    {
        public Order() { }

        [Key]
        public int Id { get; set; }
        public string TitleName { get; set; }
        public string Adress {  get; set; }
        public string Description { get; set; }
        public int DayToDelay { get; set; }
        public string ContactInformation { get; set; }
        public int UserIdCreated { get; set; }
        public int? UserIdAssigner {  get; set; }
        public string? OrderStatusId { get; set; }
        public string? OrderPriorityId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public DateTime? DateDeleted { get; set; }

        public User? UserCreator { get; set; }
        public User? UserAssigner { get; set; }
        public OrderPriority? OrderPriority { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public List<OrderScoresEmployer> OrderScoresEmployers { get; set; } = new();
        public List<OrderScoresCustomer> OrderScoresCustomers { get; set; } = new();
        public List<AssignersRequests> AssignersRequests { get; set; } = new();
        public List<RequestsToCancellation> RequestsToCancellations { get; set; } = new();
    }
}
