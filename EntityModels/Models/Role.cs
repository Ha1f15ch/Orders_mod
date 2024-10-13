using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Models
{
    [Table("Role", Schema = "dict")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public List<User> Users { get; set; } = new();
        public List<UserRole> UserRoles { get; set; } = new();
    }
}
