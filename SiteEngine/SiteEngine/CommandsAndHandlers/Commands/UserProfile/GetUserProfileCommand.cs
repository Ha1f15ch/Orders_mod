using DtoModelsProj;
using MediatR;
using SiteEngine.Models.UserProfiles;

namespace SiteEngine.CommandsAndHandlers.Commands.UserProfile
{
    public class GetUserProfileCommand : IRequest<UserProfileModel>
    {
        public int UserId { get; set; }
    }
}
