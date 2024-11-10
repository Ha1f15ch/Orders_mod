using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("OrderScoresEmployer", Schema = "dbo")]
    public class OrderScoresEmployer
    {
        public OrderScoresEmployer() { }

        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int EmployerProfileId { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public DateTime? DateDeleted { get; set; }

        public Order? Order { get; set; }
        public EmployerProfile? EmployerProfile { get; set; }
    }
}
