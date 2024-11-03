using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiteEngine.CommandsAndHandlers.Commands.UserMetadata;
using SiteEngine.CommandsAndHandlers.Commands.UserProfile;
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
            var userCookie = new UserIdMetadataCommand()
            {
                CookieString = HttpContext.Request.Cookies["access_token"]
            };

            var resultUserCookie = await mediator.Send(userCookie);

            if(resultUserCookie > 0)
            {
                var resultModel = await mediator.Send(resultUserCookie);

                return View(resultModel);
            }
            else
            {
                return RedirectToAction("CreateUserProfile", "UserProfile");
            }
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
            if (ModelState.IsValid)
            {
                var userCookie = new UserIdMetadataCommand()
                {
                    CookieString = HttpContext.Request.Cookies["access_token"]
                };

                var resultUserCookie = await mediator.Send(userCookie);

                if (resultUserCookie > 0)
                {
                    var resultModel = await mediator.Send(resultUserCookie);

                    var commandToCreateUserProfile = new UserProfileCommand
                    {
                        FirstName = model.FirstName,
                        MiddleName = model.MiddleName,
                        LastName = model.LastName,
                        Birthday = model.Birthday,
                        UserId = resultUserCookie
                    };

                    var resultCommandToCreateUserProfile = await mediator.Send(commandToCreateUserProfile);

                    if(resultCommandToCreateUserProfile)
                    {
                        return RedirectToAction("UserProfileinfo", "UserProfile");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ошибка при слздании профиля, попробуйте позднее.");
                }
            }

            return View(model);

        }

        [ServiceFilter(typeof(AuthorizeAttributeFilter))]
        [HttpGet("my-profile-update")]
        public async Task<IActionResult> UserProfileUpdate()
        {
            var userCookie = new UserIdMetadataCommand()
            {
                CookieString = HttpContext.Request.Cookies["access_token"]
            };

            var resultUserCookie = await mediator.Send(userCookie);

            if (resultUserCookie > 0)
            {
                var resultModel = await mediator.Send(resultUserCookie) as UserProfileModel;

                if(resultModel != null)
                {
                    var model = new UserProfileModel
                    {
                        FirstName = resultModel.FirstName,
                        MiddleName = resultModel.MiddleName,
                        LastName = resultModel.LastName,
                        Birthday = resultModel.Birthday,
                    };

                    return View(model);
                }
                else
                {
                    return RedirectToAction("UserProfileinfo", "UserProfile");
                }
            }
            else
            {
                return RedirectToAction("UserProfileinfo", "UserProfile");
            }
        }

        [ServiceFilter(typeof(AuthorizeAttributeFilter))]
        [HttpPut("my-profile")]
        public async Task<IActionResult> UserProfileUpdate(UserProfileModel model)
        {
            if(ModelState.IsValid)
            {
                var userCookie = new UserIdMetadataCommand()
                {
                    CookieString = HttpContext.Request.Cookies["access_token"]
                };

                var resultUserCookie = await mediator.Send(userCookie);

                if (resultUserCookie > 0)
                {
                    var commandToUpdate = new UserProfileCommand
                    {
                        FirstName = model.FirstName,
                        MiddleName = model.MiddleName,
                        LastName = model.LastName,
                        Birthday = model.Birthday,
                        UserId = resultUserCookie
                    };

                    var resultCommandToUpdate = await mediator.Send(commandToUpdate);

                    if(resultCommandToUpdate)
                    {
                        return RedirectToAction("UserProfileinfo", "UserProfile");
                    }
                }
            }
            return View(model);
        }
    }
}
