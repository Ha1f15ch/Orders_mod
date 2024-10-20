using MediatR;

namespace SiteEngine.CommandsAndHandlers.Commands.Users
{
    public class LoginUserCommand : IRequest<bool>
    {
        public string UserEmail {  get; set; }
        public string UserPassword { get; set; }
    }
}
