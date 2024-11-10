using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiteEngine.CommandsAndHandlers.Commands.UserMetadata;
using SiteEngine.CommandsAndHandlers.Commands.UserProfile;
using SiteEngine.Models.CommonDataUserProfiles;

namespace SiteEngine.Controllers.ViewModels
{
    [Route("views/main/profiles")]
    public class ProfilesController : BaseController
    {
        private readonly IMediator mediator;

        public ProfilesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("info")]
        public async Task<IActionResult> Info()
        {
            // Через куки получаем userId
            var getCookieString = new UserIdMetadataCommand()
            {
                CookieString = HttpContext.Request.Cookies["access_token"]
            };

            var getUserIdByCookieString = await mediator.Send(getCookieString);

            if(getUserIdByCookieString > 0)
            {
                // Проверяем по userId есть ли у пользователя профиль пользователя (UserProfile), если нет => редирект на создание UserProfile
                var commandForGetUserProfile = new HasUserProfileCommand //нужно создать новую команду с типом bool, если true => 
                {
                    UserId = getUserIdByCookieString
                };

                if(await mediator.Send(commandForGetUserProfile))
                {
                    var model = new CommonDataUserProfiles
                    {

                    };

                    return View(model);
                }
            }

            return RedirectToAction("CreateUserProfile", "UserProfile");
            // Проверяем по userid какие профиля есть у пользователя
            // Если нет профилей - выводим empty || Create new Profile - в новой экспортируемой модели 
            // Если есть - отображаем данные о каждом профиле (*экран разделен на 2е половины, слева Customer, справа Employer)
            // Под каждым профилем отображать update btn, delete btn => при выборе, редирект на методы ниже (Update GET, Delete GET)
        }

        [HttpGet("create-customer")]
        public async Task<IActionResult> CreateCustomer()
        {
            // Предзаполнены должны быть пересекающиеся части между UserProfile и CustomerProfile
        }

        [HttpPost("create-customer")]
        public async Task<IActionResult> CreateCustomer()
        {
            
        }

        [HttpGet("create-employer")]
        public async Task<IActionResult> CreateEmployer()
        {
            // Предзаполнены должны быть пересекающиеся части между UserProfile и EmployerProfile
        }

        [HttpPost("create-employer")]
        public async Task<IActionResult> CreateEmployer()
        {

        }
    }
}
