using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("EmployerProfile", Schema = "dbo")]
    public class EmployerProfile
    {
        public EmployerProfile() { }

        [Key]
        public int Id { get; set; }
        public bool IsActived { get; set; } = true;
        public int UserId { get; set; }

        public User User { get; set; }
        public List<OrderScoresEmployer> OrderScoresEmployers { get; set; } = new();
    }
}
