using MediatR;
using SiteEngine.CommandsAndHandlers.DtoModels;

namespace SiteEngine.CommandsAndHandlers.Commands.Tokens
{
    public class GenerateTokensQuery : IRequest<TokensDto>
    {
        public int UserId { get; set; }
    }
}
