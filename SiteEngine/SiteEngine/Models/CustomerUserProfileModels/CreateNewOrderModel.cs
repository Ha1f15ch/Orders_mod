using System.ComponentModel.DataAnnotations;

namespace SiteEngine.Models.CustomerUserProfileModels
{
    public class CreateNewOrderModel
    {
        [Required(ErrorMessage = "Закловок заказа обязателен для заполненеия !")]
        public string TitleName { get; set; }
        [Required(ErrorMessage = "Адрес заказа обязателен для заполненеия !")]
        public string Adress { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Время заказа обязателено для заполненеия !")]
        [Range(1, 14, ErrorMessage = "Время выполнения заказа не может быть меньше 1 и больше 14 !")]
        public int DayToDelay { get; set; }
        [Required(ErrorMessage = "Поле для ввода контактной информации обязательно для заполненеия !")]
        public string ContactInformation { get; set; }
        [Required(ErrorMessage = "Поле для ввода приоритета заказа обязательно для заполненеия !")]
        public string OrderPriorityId { get; set; }
    }
}
