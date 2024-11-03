using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiteEngine.Middlewares;
using SiteEngine.Models.UserProfiles;

namespace SiteEngine.Controllers.ViewModels
{
    [Route("views/main/")]
    public class UserProfileController : BaseController
    {
        private readonly IMediator mediator;

        public UserProfileController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [ServiceFilter(typeof(AuthorizeAttributeFilter))]
        [HttpGet("my-profile")]
        public async Task<IActionResult> UserProfileinfo()
        {
            //если пользователь только что зарегистрировался - необходимо заполнить профиль - 1 редирект на страницу для создания/заполнения профиля
            if()
            {
                //если нет результата по userProfileModel - редлирект на создание
            }
            //Если у пользователя уже заполнен профиль ранее, то он его получает
            //как понять что этот тот самый пользователь ? 
            //После авторизации - нужно знать его user Id или, если авторизован userProfileId
            //используя id через медиатр вытянуть данные
            //Так как это уже заполненный профиль, имеет смысл не создавать отдельно окно с get update запросом 
            //Возможно, имеет смысл передавать не userProfile из EFC, а UserProfileModel
            return View();
        }

        [HttpGet("create-my-profile")]
        public async Task<IActionResult> CreateUserProfile()
        {
            return View();
        }

        [ServiceFilter(typeof(AuthorizeAttributeFilter))]
        [HttpPost("create-my-profile")]
        public async Task<IActionResult> CreateUserProfile(UserProfileModel model)
        {

        }

        [ServiceFilter(typeof(AuthorizeAttributeFilter))]
        [HttpPut("my-profile")]
        public async Task<IActionResult> UserProfileUpdate(UserProfileModel model)
        {
            if(ModelState.IsValid)
            {

            }

            return RedirectToAction("UserProfileinfo", "UserProfile");
        }
    }
}
