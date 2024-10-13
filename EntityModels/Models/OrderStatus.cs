using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("OrderStatus", Schema = "meta")]
    public class OrderStatus
    {
        public OrderStatus() { }
        [Key]
        public string OrderStatusId { get; set; } 
        public string? Description { get; set; }

        public List<Order> Orders { get; set; } = new();
    }
}
