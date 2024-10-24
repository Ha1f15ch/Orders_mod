using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiteEngine.CommandsAndHandlers.Commands.Tokens;
using SiteEngine.CommandsAndHandlers.Commands.Users;
using SiteEngine.Interfaces;
using SiteEngine.Interfaces.AuthMethodsControllers;
using SiteEngine.Interfaces.AuthorizationInterfaces;
using SiteEngine.Models.UserAccount;

namespace SiteEngine.Controllers.ViewModels
{
    [Route("views/main/account/")]
    public class AuthorizationController : BaseController, IRegistration, ILogin
    {
        private readonly IMediator mediator;

        public AuthorizationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("registration")]
        public async Task<IActionResult> UserRegistration()
        {
            return View();
        }

        [HttpPost("registration")]
        public async Task<IActionResult> UserRegistration(UserRegistrationModel model)
        {
            if(ModelState.IsValid)
            {
                var userRegisterCommand = new RegisterUserCommand 
                { 
                    UserName = model.UserName, 
                    UserEmail = model.UserEmail, 
                    UserPassword = model.UserPassword, 
                    UserPhoneNumber = model.UserPhoneNumber 
                };

                var resultCommand = await mediator.Send(userRegisterCommand);

                if (resultCommand)
                {
                    
                    return RedirectToAction("login", "Authorization");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ошибка при регистрации. Попробуйте еще раз.");
                }
            }

            return View(model);
        }

        [HttpGet("login")]
        public async Task<IActionResult> UserLogin()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin(UserLoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var command = new LoginUserCommand
            {
                UserEmail = model.UserEmail,
                UserPassword = model.UserPassword
            };

            var result = await mediator.Send(command);

            if(!result.IsSuccess)
            {
                return Unauthorized(result.ErrorMessage);
            }

            var commandForToken = new GenerateTokensQuery
            {
                UserId = result.UserId
            };

            var resultForToken = await mediator.Send(commandForToken);

            Response.Cookies.Append("access_token", resultForToken.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(30)
            });

            return RedirectToAction("MainPage", "Main");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("access_token");
            return RedirectToAction("MainPage", "Main");
        }
    }
}
