using Microsoft.EntityFrameworkCore;
using DatabaseContext;
using Microsoft.OpenApi.Models;
using MediatR;
using SiteEngine.CommandsAndHandlers.Handlers.Users;
using System.Reflection;
using Repositories.InterfaceRepositories;
using Repositories.Repositories;
using System.Text;
using ServiceStack;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SiteEngine.Middlewares;
using SiteEngine.CommandsAndHandlers.Handlers.Tokens;
using SiteEngine.CommandsAndHandlers.Commands.Tokens;
using SiteEngine.CommandsAndHandlers.DtoModels;
using Repositories.InterfaceForServices;
using Repositories.Services;

namespace SiteEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Add services to the container.
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            var key = Encoding.ASCII.GetBytes("MySecretString");
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "Issuer",
                    ValidAudience = "Audience"
                };

            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddTransient<IUserAccauntRepository, UserAccauntRepository>();
            builder.Services.AddScoped<ITokenRepository, TokenRepository>();
            builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();
            builder.Services.AddTransient<IUserProfileRepository, UserProfileRepository>();
            builder.Services.AddTransient<IDecodeAndVerifieCookieService, DecodeAndVerifieCookieService>();
            builder.Services.AddTransient<ICommonProfileData, CommonProfileData>();
            builder.Services.AddScoped<AuthorizeAttributeFilter>();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<RegisterUserCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<LoginUserCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<GenerateTokensCommandHandler>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.Map("/", async context => context.Response.Redirect("/views/main"));

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API documentation for View models routs");
                c.RoutePrefix = "swagger";
            });

            app.MapRazorPages();
            app.MapControllers();

            app.Run();
        }
    }
}
