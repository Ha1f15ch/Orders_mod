using DatabaseContext;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repositories.InterfaceRepositories;
using SiteEngine.CommandsAndHandlers.DtoModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;

        public TokenRepository(AppDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public Task<string> CreateJwtToken(int userId, TimeSpan expiry)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        }

        public Task<TokensDto> GenerateTokens(string userId)
        {
            throw new NotImplementedException();
        }

        public Task SaveToken(int userId, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task SetTokensInResponse(TokensDto tokens)
        {
            throw new NotImplementedException();
        }
    }
}
