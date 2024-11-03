using System.ComponentModel.DataAnnotations;

namespace SiteEngine.Models.UserProfiles
{
    public class UserProfileModel
    {
        [Required(ErrorMessage = "Имя обязательно для заполненеия.")]
        [StringLength(50, ErrorMessage = "Имя не может быть длиннее 50 символов.")]
        public string FirstName { get; set; }
        [StringLength(50, ErrorMessage = "Отчетсво не может быть длиннее 50 символов.")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Фамилия обязательна для заполненеия.")]
        [StringLength(50, ErrorMessage = "Фамилия не может быть длиннее 50 символов.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Дата рождения обязательна.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [CustomAgeValidator(MinimumAge = 13, ErrorMessage = "Вам должно быть больше 13 лет.")]
        public DateTime Birthday { get; set; }
    }

    public class CustomAgeValidator : ValidationAttribute
    {
        public int MinimumAge { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime birthday)
            {
                int age = DateTime.Today.Year - birthday.Year;
                if (birthday.Date > DateTime.Today.AddYears(-age)) age--;

                if (age < MinimumAge)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }
    }
}
