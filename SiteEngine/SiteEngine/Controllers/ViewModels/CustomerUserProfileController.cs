using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiteEngine.CommandsAndHandlers.Commands.UserMetadata;
using SiteEngine.Models.CustomerUserProfileModels;

namespace SiteEngine.Controllers.ViewModels
{
    [Route("views/customer-main/")]
    public class CustomerUserProfileController : BaseController
    {
        private readonly IMediator mediator;

        public CustomerUserProfileController(IMediator mediator)
        {
            this.mediator = mediator;
        }

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

        [HttpGet("new-order")]
        public async Task<IActionResult> CreateNewOrder()
        {
            return View();
        }

        [HttpPost("new-order")]
        public async Task<IActionResult> CreateNewOrder(CreateNewOrderModel model)
        {
            if(ModelState.IsValid)
            {
                var commandForGetCookieString = new UserIdMetadataCommand()
                {
                    CookieString = HttpContext.Request.Cookies["access_token"]
                };

                var userIdByCookieString = await mediator.Send(commandForGetCookieString);
            }

            return View(model);
        }
    }
}
