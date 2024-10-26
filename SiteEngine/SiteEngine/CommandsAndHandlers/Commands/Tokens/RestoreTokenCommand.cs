using MediatR;

namespace SiteEngine.CommandsAndHandlers.Commands.Tokens
{
    public class RestoreTokenCommand : IRequest<string>
    {
        public string AccessTokenForRestore { get; set; }
    }
}
