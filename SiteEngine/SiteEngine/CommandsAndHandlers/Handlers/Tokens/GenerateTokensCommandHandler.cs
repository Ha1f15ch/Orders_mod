using Repositories.InterfaceRepositories;
using SiteEngine.CommandsAndHandlers.Commands.Tokens;
using SiteEngine.CommandsAndHandlers.DtoModels;

namespace SiteEngine.CommandsAndHandlers.Handlers.Tokens
{
    public class GenerateTokensCommandHandler
    {
        private readonly ITokenRepository tokenRepository;
        
        public GenerateTokensCommandHandler(ITokenRepository tokenRepository)
        {
            this.tokenRepository = tokenRepository;
        }

        public async Task<TokensDto> Handle(GenerateTokensQuery request, CancellationToken cancellationToken)
        {
            var accessToken = await tokenRepository.GenerateJwtToken(request.UserId);
            var refreshToken = await tokenRepository.GenerateJwtToken(request.UserId);

            return new TokensDto
            {

            };
        }
    }
}
