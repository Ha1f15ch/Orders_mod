using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("AssignersRequests", Schema = "dbo")]
    public class AssignersRequests //Заявки пользователй на испольнение заказа
    {
        public AssignersRequests() { }

        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateDeleted { get; set; }

        public Order? Order { get; set; }
        public User? User { get; set; }
    }
}
