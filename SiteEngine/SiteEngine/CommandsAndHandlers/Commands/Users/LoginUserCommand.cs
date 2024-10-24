using MediatR;

namespace SiteEngine.CommandsAndHandlers.Commands.Users
{
    public class LoginUserCommand : IRequest<LoginUserResult>
    {
        public string UserEmail {  get; set; }
        public string UserPassword { get; set; }
    }
}
