using Microsoft.AspNetCore.Mvc;
using SiteEngine.Models.UserAccount;

namespace SiteEngine.Interfaces.AuthorizationInterfaces
{
    public interface ILogin
    {
        public Task<IActionResult> UserLogin(UserLoginModel model);
    }
}
