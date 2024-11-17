using Microsoft.AspNetCore.Mvc;

namespace SiteEngine.Controllers.ViewModels
{
    [Route("views/employer-main/")]
    public class EmployerUserProfileController : BaseController
    {
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet("profile")]
        public async Task<IActionResult> ProfileEmployer()
        {
            return View();
        }
    }
}
