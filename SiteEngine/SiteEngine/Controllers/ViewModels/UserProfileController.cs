using DtoModelsProj;
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
                var userProfileCommand = new GetUserProfileCommand
                {
                    UserId = resultUserCookie,
                };

                var resultModel = await mediator.Send(userProfileCommand);

                if(resultModel != null)
                {
                    return View(resultModel);
                } 
            }

            return RedirectToAction("CreateUserProfile");
        }

        [HttpGet("create-my-profile")]
        public async Task<IActionResult> CreateUserProfile()
        {
            var model = new UserProfileModel();

            return View(model);
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
                var commandForGetUserProfileModel = new GetUserProfileCommand
                {
                    UserId = resultUserCookie
                };

                var resultModel = await mediator.Send(commandForGetUserProfileModel);

                if(resultModel != null)
                {
                    var model = new UserProfileModel
                    {
                        FirstName = resultModel.UserProfileFirstName,
                        MiddleName = resultModel.UserProfileMiddleName,
                        LastName = resultModel.UserProfileLastName,
                        Birthday = resultModel.UserProfileBirthday,
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
        [HttpPost("my-profile-update")]
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
                    var commandToUpdate = new UserProfileUpdateCommand
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
