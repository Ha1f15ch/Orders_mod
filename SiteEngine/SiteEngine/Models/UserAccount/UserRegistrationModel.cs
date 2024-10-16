using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = ServiceStack.DataAnnotations.RequiredAttribute;

namespace SiteEngine.Models.UserAccount
{
    public class UserRegistrationModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        [MinLength(6)]
        public string UserPassword { get; set; }

        [Phone]
        public string UserPhoneNumber { get; set; }
    }
}
