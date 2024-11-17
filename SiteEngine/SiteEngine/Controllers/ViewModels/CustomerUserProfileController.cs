using Microsoft.AspNetCore.Mvc;

namespace SiteEngine.Controllers.ViewModels
{
    [Route("views/customer-main/")]
    public class CustomerUserProfileController : BaseController
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/profile")]
        public IActionResult ProfileCustomer()
        {
            return View();
        }
    }
}
