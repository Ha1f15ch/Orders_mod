using Models;
using SiteEngine.CommandsAndHandlers.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.InterfaceRepositories
{
    public interface ITokenRepository
    {
        public Task<TokensDto> GenerateTokens(string userId);
        public Task<string> CreateJwtToken(int userId, TimeSpan expiry);
        public Task SetTokensInResponse(TokensDto tokens);
        public Task SaveToken(int userId, string refreshToken);
    }
}
