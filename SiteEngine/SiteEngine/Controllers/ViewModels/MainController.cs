using Microsoft.AspNetCore.Mvc;

namespace SiteEngine.Controllers.ViewModels
{
    [Route("/views/main")]
    public class MainController : Controller
    {
        [HttpGet("")]
        public async Task<IActionResult> MainPage()
        {
            return View();
        }
    }
}
