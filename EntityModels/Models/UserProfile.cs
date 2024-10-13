using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("UserProfile", Schema = "dbo")]
    public class UserProfile
    {
        public UserProfile() { }

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - Birthday.Year;

                if (Birthday.Date > today.AddYears(-age)) age--;

                return age;
            }
        }
        public bool IsActived { get; set; } = true;
        public DateTime DateCreatedAt { get; set; }
        public DateTime DateUpdatedAt { get; set; }
        public DateTime? DateDelete {  get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
