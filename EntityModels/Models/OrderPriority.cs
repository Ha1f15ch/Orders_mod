using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("OrderPriority", Schema = "meta")]
    public class OrderPriority
    {
        public OrderPriority() { }

        [Key]
        public string OrderPriorityId { get; set; }
        public string? Description { get; set; }

        public List<Order> Orders { get; set; } = new();
    }
}
