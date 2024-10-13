using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Role", Schema = "dict")]
    public class Role
    {
        public Role() { }

        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public List<User> Users { get; set; } = new();
        public List<UserRole> UserRoles { get; set; } = new();
    }
}
