using DtoModelsProj;
using MediatR;
using SiteEngine.Models.UserProfiles;

namespace SiteEngine.CommandsAndHandlers.Commands.UserProfile
{
    public class GetUserProfileCommand : IRequest<UserProfileModelDto>
    {
        public int UserId { get; set; }
    }
}
