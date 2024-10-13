using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int TimeForWork { get; set; }
        public int UserIdCreated { get; set; }
        public int UserIdAssigner {  get; set; }
        public string? OrderStatusId { get; set; }
        public string? OrderPriorityId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public DateTime? DateDeleted { get; set; }

        public OrderPriority? OrderPriority { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public List<OrderScores> OrderScores { get; set; } = new();
        public List<AssignersRequests> AssignersRequests { get; set; } = new();
        public List<RequestsToCancellation> RequestsToCancellations { get; set; } = new();
    }
}
