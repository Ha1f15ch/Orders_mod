using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("Token", Schema = "dbo")]
    public class Token
    {
        public Token() { }

        [Key]
        public Guid Guid { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime DateExpired {  get; set; } 

        public User? User { get; set; }
    }
}
