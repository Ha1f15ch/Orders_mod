using Microsoft.AspNetCore.Mvc;
using SiteEngine.Interfaces;
using SiteEngine.Interfaces.AuthMethodsControllers;

namespace SiteEngine.Controllers.ViewModels
{
    [Route("views/main/account/")]
    public class AuthorizationController : BaseController, IRegistration
    {
        [HttpGet("registration")]
        public async Task<IActionResult> UserRegistration()
        {
            return View();
        }

        [HttpPost("registration")]
        public Task<IActionResult> UserRegistration(string user_name, string user_email, string user_password, string user_phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
