using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("AssignersRequests", Schema = "dbo")]
    public class AssignersRequests
    {
        public AssignersRequests() { }

        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateDeleted { get; set; }

        public Order? Order { get; set; }
        public User? User { get; set; }
    }
}
