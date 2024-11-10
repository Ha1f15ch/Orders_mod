using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("OrderScoresCustomer", Schema = "dbo")]
    public class OrderScoresCustomer
    {
        public OrderScoresCustomer() { }

        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerProfileId { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public DateTime? DateDeleted { get; set; }

        public Order? Order { get; set; }
        public CustomerProfile? CustomerProfile { get; set; }
    }
}
