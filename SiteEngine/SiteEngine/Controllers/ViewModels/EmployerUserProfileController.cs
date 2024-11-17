using Microsoft.AspNetCore.Mvc;

namespace SiteEngine.Controllers.ViewModels
{
    [Route("views/employer-main/")]
    public class EmployerUserProfileController : BaseController
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/profile")]
        public IActionResult ProfileEmployer()
        {
            return View();
        }
    }
}
