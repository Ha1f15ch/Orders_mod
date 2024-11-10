using Microsoft.AspNetCore.Mvc;
using SiteEngine.Middlewares;

namespace SiteEngine.Controllers.ViewModels
{
    [Route("/views/main")]
    public class MainController : Controller
    {
        [ServiceFilter(typeof(AuthorizeAttributeFilter))]
        [HttpGet("")]
        public async Task<IActionResult> MainPage()
        {
            return View();
        }
    }
}
