using Microsoft.IdentityModel.Tokens;
using Models;
using Repositories.InterfaceRepositories;
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
        public async Task<string> GenerateJwtToken(User user)
        {
            
        }
    }
}
