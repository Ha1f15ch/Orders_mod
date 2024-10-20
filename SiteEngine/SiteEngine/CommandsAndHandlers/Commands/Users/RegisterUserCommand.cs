using MediatR;

namespace SiteEngine.CommandsAndHandlers.Commands.Users
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserPhoneNumber { get; set; }
    }
}
