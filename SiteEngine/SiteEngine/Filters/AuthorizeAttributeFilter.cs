using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SiteEngine.Middlewares
{
    public class AuthorizeAttributeFilter : ActionFilterAttribute
    {
        private readonly IConfiguration _configuration;

        public AuthorizeAttributeFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Cookies["access_token"];

            if (token == null)
            {
                context.Result = new RedirectToActionResult("UserLogin", "Authorization", null);
                return;
            }

            var principal = ValidateToken(token, _configuration);
            if (principal == null)
            {
                context.Result = new RedirectToActionResult("RestoreAccessToken", "Authorization", null);
                return;
            }

            context.HttpContext.User = principal;
            base.OnActionExecuting(context);
        }

        private ClaimsPrincipal ValidateToken(string token, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"]);

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
                return tokenHandler.ValidateToken(token, TokenValidateionParameters, out var validatedToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"токен не дейсвителен. {ex.Message}");
                return null;
            }
        }
    }
}
