using Models;

namespace SiteEngine.Models.CommonDataUserProfiles
{
    public class CommonDataUserProfiles
    {
        public int UserId { get; set; }
        public UserProfile? UserProfile { get; set; }
        public CustomerProfile? CustomerProfile { get; set; }
        public EmployerProfile? EmployerProfile { get; set; }
        //Оценки, рейтинг по заказам
    }
}
