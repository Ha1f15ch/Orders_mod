using Models;

namespace SiteEngine.Models.CommonDataUserProfiles
{
    public class CommonDataUserProfiles
    {
        int UserId { get; set; }
        UserProfile UserProfile { get; set; } = null!;
        CustomerProfile? CustomerProfile { get; set; }
        EmployerProfile? EmployerProfile { get; set; }
        //Оценки, рейтинг по заказам
    }
}
