using MediatR;

namespace SiteEngine.CommandsAndHandlers.Commands.UserProfile
{
    public class HasUserProfileCommand : IRequest<bool>
    {
        public int UserId { get; set; }
    }
}
