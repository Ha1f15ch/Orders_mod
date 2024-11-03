using MediatR;
using Repositories.InterfaceForServices;
using SiteEngine.CommandsAndHandlers.Commands.UserMetadata;

namespace SiteEngine.CommandsAndHandlers.Handlers.UserMetadata
{
    public class UserIdMetadataCommandHandler : IRequestHandler<UserIdMetadataCommand, int>
    {
        private readonly IDecodeAndVerifieCookieService decodeAndVerifieCookieService;

        public UserIdMetadataCommandHandler(IDecodeAndVerifieCookieService decodeAndVerifieCookieService)
        {
            this.decodeAndVerifieCookieService = decodeAndVerifieCookieService;
            Console.WriteLine("UserIdMetadataCommandHandler создан");
        }

        public async Task<int> Handle(UserIdMetadataCommand request, CancellationToken cancellationToken)
        {
            var intForResult = await decodeAndVerifieCookieService.GetUserIdFromCookie(request.CookieString);

            if(intForResult > 0)
            {
                return intForResult;
            }
            else
            {
                return 0;
            }
        }
    }
}
