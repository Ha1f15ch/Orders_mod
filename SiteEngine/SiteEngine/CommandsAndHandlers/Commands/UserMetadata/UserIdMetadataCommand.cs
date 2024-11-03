using MediatR;

namespace SiteEngine.CommandsAndHandlers.Commands.UserMetadata
{
    public class UserIdMetadataCommand : IRequest<int>
    {
        public string CookieString { get; set; }
    }
}
