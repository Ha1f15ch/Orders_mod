using DatabaseContext;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Services
{
    public class DecodeAndVerifieCookieService
    {
        private readonly AppDbContext context;

        public DecodeAndVerifieCookieService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> GetUserIdFromCookie(string tokenValue)
        {
            if(string.IsNullOrEmpty(tokenValue))
            {
                return 0;
            }
            else
            {
                var handler = new JwtSecurityTokenHandler();

                if(handler.ReadToken(tokenValue) is JwtSecurityToken jwtToken)
                {
                    var useridClaims = jwtToken.Claims.First(claims => claims.Type == JwtRegisteredClaimNames.Sub);
                    if (int.TryParse(useridClaims.Value, out var userId))
                    {
                        return userId;
                    }
                }
                
                return 0;
            }
        }
    }
}
