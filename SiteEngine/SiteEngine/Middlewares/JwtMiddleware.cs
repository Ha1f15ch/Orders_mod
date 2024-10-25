using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SiteEngine.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this.next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Cookies["access_token"];

            if(!(context.Request.Path == "/views/main") &&
                !context.Request.Path.StartsWithSegments("/css") &&
                !context.Request.Path.StartsWithSegments("/js"))
            {
                if (token == null)
                {
                    context.Response.Redirect("views/main/account/login");
                    return;
                }

                var principal = ValidateToken(token);

                if (principal == null)
                {
                    context.Response.Redirect("views/main/account/login");
                    return;
                }

                Console.WriteLine($"Токен валиден, доступ разрешен. \n Token - {token} \n Path - {context.Request.Path}");
            }

            await next(context);
        }

        private ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

            var TokenValidateionParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
            };

            try
            {
                return tokenHandler.ValidateToken(token,TokenValidateionParameters, out var validatedToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"токен не дейсвителен. {ex.Message}");
                return null;
            }
        }
    }
}
