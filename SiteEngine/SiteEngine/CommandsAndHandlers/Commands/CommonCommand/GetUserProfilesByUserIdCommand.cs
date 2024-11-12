using MediatR;

namespace SiteEngine.CommandsAndHandlers.Commands.CommonCommand
{
    public class GetUserProfilesByUserIdCommand : IRequest<IDictionary<string, object?>>
    {
        public int UserId { get; set; }
    }
}
