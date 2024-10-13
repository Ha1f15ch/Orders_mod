using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("RequestsToCancellation", Schema = "dbo")]
    public class RequestsToCancellation
    {
        public RequestsToCancellation() { }

        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int InitiatorUserId { get; set; }
        public int ConfirmedUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateDeleted { get; set; }

        public Order? Order { get; set; }
    }
}
