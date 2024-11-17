using Microsoft.AspNetCore.Mvc;

namespace SiteEngine.Controllers.ViewModels
{
    [Route("views/customer-main/")]
    public class CustomerUserProfileController : BaseController
    {
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet("profile")]
        public async Task<IActionResult> ProfileCustomer()
        {
            return View();
        }
    }
}
