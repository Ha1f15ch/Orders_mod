using Microsoft.AspNetCore.Mvc;
using SiteEngine.Models.UserAccount;

namespace SiteEngine.Interfaces.AuthMethodsControllers
{
    public interface IRegistration
    {
        public Task<IActionResult> UserRegistration(UserRegistrationModel model);
    }
}
