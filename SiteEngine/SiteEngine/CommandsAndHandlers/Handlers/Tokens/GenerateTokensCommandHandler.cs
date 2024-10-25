using MediatR;
using Repositories.InterfaceRepositories;
using SiteEngine.CommandsAndHandlers.Commands.Tokens;
using SiteEngine.CommandsAndHandlers.DtoModels;

namespace SiteEngine.CommandsAndHandlers.Handlers.Tokens
{
    public class GenerateTokensCommandHandler : IRequestHandler<GenerateTokensQuery, TokensDto>
    {
        private readonly IMediator mediator;
        private readonly ITokenRepository tokenRepository;
        private readonly IUserAccauntRepository userAccauntRepository;
        
        public GenerateTokensCommandHandler(IMediator mediator, ITokenRepository tokenRepository, IUserAccauntRepository userAccauntRepository)
        {
            this.mediator = mediator;
            this.tokenRepository = tokenRepository;
            this.userAccauntRepository = userAccauntRepository;
        }

        public async Task<TokensDto> Handle(GenerateTokensQuery request, CancellationToken cancellationToken)
        {
            var accessToken = await tokenRepository.GenerateJwtToken(request.UserId);
            var refreshToken = await tokenRepository.GenerateRefreshToken(request.UserId);

            return new TokensDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }
    }
}
