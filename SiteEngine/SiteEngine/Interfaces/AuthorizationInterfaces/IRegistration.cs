using Microsoft.AspNetCore.Mvc;

namespace SiteEngine.Interfaces.AuthMethodsControllers
{
    public interface IRegistration
    {
        public Task<IActionResult> UserRegistration(string user_name, string user_email, string user_password, string user_phoneNumber);
    }
}
