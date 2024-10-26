using DatabaseContext;
using Microsoft.EntityFrameworkCore;
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
                        expires: DateTime.UtcNow.AddMinutes(30),
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
                    if(await GetRefreshTokenByUserId(userId) == null)
                    {
                        var refreshToken = Guid.NewGuid().ToString();

                        var newTokenModel = new Token
                        {
                            UserId = userId,
                            TokenValue = refreshToken,
                            DateCreate = DateTime.UtcNow,
                            DateExpired = DateTime.UtcNow.AddDays(1),
                        };

                        await context.Tokens.AddAsync(newTokenModel);
                        await context.SaveChangesAsync();

                        Console.WriteLine($"Сгененрирован новый refreshToken - {refreshToken} \n Записано в БД: \n UserId = {newTokenModel.UserId} \n TokenValue = {newTokenModel.TokenValue} \n DateCreate = {newTokenModel.DateCreate} \n DateExpired = {newTokenModel.DateExpired}");

                        return refreshToken;
                    }
                    else
                    {
                        var existingRefreshToken = await GetRefreshTokenByUserId(userId);

                        if(await CheckValidRefreshToken(existingRefreshToken))
                        {
                            return existingRefreshToken.TokenValue;
                        }
                        else
                        {
                            if(await RemoveRefreshTokenByTokenId(existingRefreshToken.Guid))
                            {
                                return string.Empty;
                            }
                            else
                            {
                                Console.WriteLine($"рещультат выполнения операции - RemoveRefreshTokenByTokenId = false");
                                throw new ArgumentException($"Токен refresh - просрочен. При выполнении удаления RemoveRefreshTokenByTokenId возникла ошибка.");
                            }
                        }
                    }
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

        public async Task<string> RestoreAccessToken(string token)
        {
            try
            {
                var userId = await GetUserIdFromExpiredToken(token);
                var refreshToken = await GetRefreshTokenByUserId(userId);

                if ((await context.Users.AnyAsync(el => el.Id == userId)) && await CheckValidRefreshToken(refreshToken))
                {
                    return await GenerateJwtToken(userId);
                }
                else
                {
                    Console.WriteLine("RefreshToken или пользователь не найдены");
                    await RemoveRefreshTokenByTokenId(refreshToken.Guid);
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении accessToken - {ex.Message}");
                return string.Empty;
            }
        }

        public async Task<Token?> GetRefreshTokenByUserId(int userId)
        {
            try
            {
                if(userId > 0)
                {
                    return await context.Tokens.SingleOrDefaultAsync(el => el.UserId == userId);
                }
                else
                {
                    Console.WriteLine($"Передано некорректное значение userId = {userId}. \n Не должно быть равно или меньше 0");
                    throw new ArgumentException("Значение аргумента userId меньше, либо равно 0");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникла ошибка при поиске записи token - {ex.Message}");
                return null; 
            }
        }

        public async Task<Token?> GetRefreshTokenByTokenId(Guid tokenId)
        {
            try
            {
                if (!(tokenId == Guid.Empty))
                {
                    return await context.Tokens.FindAsync(tokenId);
                }
                else 
                {
                    Console.WriteLine($"Передано некорректное значение tokenId = {tokenId}. \n Не должно быть пустым или равно null");
                    throw new ArgumentException("Значение аргумента tokenId отсутствует или равно null.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникла ошибка при поиске записи token - {ex.Message}");
                return null;
            }
        }

        public async Task<bool> RemoveRefreshTokenByTokenId(Guid tokenId)
        {
            try
            {
                var entityForRemove = await GetRefreshTokenByTokenId(tokenId);

                if (entityForRemove != null)
                {
                    context.Tokens.Remove(entityForRemove);
                    await context.SaveChangesAsync();

                    Console.WriteLine($"Успешно удалено: entityForRemove = {entityForRemove} \n entityForRemove.Guid = {entityForRemove.Guid} \n entityForRemove.UserId = {entityForRemove.UserId} \n entityForRemove.TokenValue = {entityForRemove.TokenValue} \n entityForRemove.DateCreate = {entityForRemove.DateCreate} \n entityForRemove.DateExpired = {entityForRemove.DateExpired}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Hе была найдена сущность для удаления");
                    throw new ArgumentException($"ошибка, значение для удаления null. \n entityForRemove {entityForRemove}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникла ошибка при удалении записи token, error - {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RemoveRefreshTokenByUserId(int userId)
        {
            try
            {
                if (userId > 0)
                {
                    var entityForRemove = await GetRefreshTokenByUserId(userId);
                    if(entityForRemove != null)
                    {
                        context.Tokens.Remove(entityForRemove);
                        await context.SaveChangesAsync();

                        Console.WriteLine($"Успешно удалено: entityForRemove = {entityForRemove} \n entityForRemove.Guid = {entityForRemove.Guid} \n entityForRemove.UserId = {entityForRemove.UserId} \n entityForRemove.TokenValue = {entityForRemove.TokenValue} \n entityForRemove.DateCreate = {entityForRemove.DateCreate} \n entityForRemove.DateExpired = {entityForRemove.DateExpired}");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Не найдено значения token для удаления. \n userId = {userId} \n entityForRemove = {entityForRemove}");
                        throw new ArgumentException($"Ошибка при удалении записи token. token = {entityForRemove}");
                    }
                }
                else
                {
                    Console.WriteLine($"Передано некорректное значение для удаления token: userId = {userId}. \n Не должно быть равно или меньше 0");
                    throw new ArgumentException("Значение аргумента userId меньше, либо равно 0 \n Удаление невозможно");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникла ошибка при удалении записи token, error - {ex.Message}");
                return false;
            }
        }

        private async Task<int> GetUserIdFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(configuration["Jwt:SecretKey"])),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var userIdClaim = principal.FindFirst("userId");
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }
            }
            catch( Exception ex )
            {
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return 0;
            };

            return 0;
        }

        private async Task<bool> CheckValidRefreshToken(Token refreshToken)
        {
            try
            {
                if(refreshToken.DateExpired < DateTime.UtcNow)
                {
                    Console.WriteLine($"refreshToken не валиден, просрочен. \n текущая дата = {DateTime.UtcNow} \n refreshToken.DateExpired = {refreshToken.DateExpired}");
                    return false;
                }
                else
                {
                    Console.WriteLine($"Токен валиден: \n текущая дата = {DateTime.UtcNow} \n refreshToken.DateExpired = {refreshToken.DateExpired}");
                    return true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Bозникла ошибка при определии активности токена. Error: {ex.Message}");
                return false;
            }
        }
    }
}
