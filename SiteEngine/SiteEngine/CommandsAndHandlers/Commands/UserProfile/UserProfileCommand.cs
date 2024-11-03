using MediatR;

namespace SiteEngine.CommandsAndHandlers.Commands.UserProfile
{
    public class UserProfileCommand : IRequest<bool>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public int UserId { get; set; }
    }
}
