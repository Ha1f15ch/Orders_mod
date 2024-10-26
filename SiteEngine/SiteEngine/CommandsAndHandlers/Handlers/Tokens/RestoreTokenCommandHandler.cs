using MediatR;
using Microsoft.IdentityModel.Tokens;
using Repositories.InterfaceRepositories;
using SiteEngine.CommandsAndHandlers.Commands.Tokens;

namespace SiteEngine.CommandsAndHandlers.Handlers.Tokens
{
    public class RestoreTokenCommandHandler : IRequestHandler<RestoreTokenCommand, string>
    {
        private readonly ITokenRepository tokenRepository;

        public RestoreTokenCommandHandler(ITokenRepository tokenRepository)
        {
            Console.WriteLine($"RestoreTokenCommandHandler создан");
            this.tokenRepository = tokenRepository;
        }

        public async Task<string> Handle(RestoreTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var restoredToken = await tokenRepository.RestoreAccessToken(request.AccessTokenForRestore);
                if (!restoredToken.IsNullOrEmpty())
                {
                    return restoredToken;
                } 
                else
                {
                    Console.WriteLine("RestoreTokenCommandHandler - При обновлении токена доступа accessToken возникла ошибка. ");
                    return string.Empty;
                }
            }
            catch ( Exception ex )
            {
                Console.WriteLine($"При выполнении команды - RestoreTokenCommandHandler возникла ошибка: {ex.Message}");
                return string.Empty;
            }
        }

    }
}
