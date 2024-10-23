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

        public async Task<string> GenerateJwtToken(int userId)
        {
            try
            {
                if (userId > 0)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: configuration["Jwt:Issuer"],
                        audience: configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds);

                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
                else
                {
                    Console.WriteLine($"Передано в userId - {userId}");
                    throw new ArgumentException($"Для генерации JwtToken передано некорректное значение - userId = {userId}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении метода - GenerateJwtToken. \n {ex.Message}");
                return String.Empty;
            }
        }

        public async Task<string> GenerateRefreshToken(int userId)
        {
            try
            {
                if(userId > 0)
                {
                    var refreshToken = Guid.NewGuid().ToString();

                    var newTokenModel = new Token
                    {
                        UserId = userId,
                        TokenValue = refreshToken,
                        DateCreate = DateTime.Now,
                        DateExpired = DateTime.Now.AddDays(1),
                    };

                    await context.Tokens.AddAsync(newTokenModel);
                    await context.SaveChangesAsync();

                    return refreshToken;
                }
                else
                {
                    Console.WriteLine($"Передано в userId - {userId}");
                    throw new ArgumentException($"Для генерации RefreshToken передано некорректное значение - userId = {userId}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении метода - GenerateRefreshToken. \n {ex.Message}");
                return String.Empty;
            }
        }
    }
}
