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
using SiteEngine.CommandsAndHandlers.Handlers.CommonCommandHandlers;
using SiteEngine.CommandsAndHandlers.Handlers.CustomerCommandsHandlers;
using SiteEngine.CommandsAndHandlers.Handlers.EmployerCommandsHandlers;
using SiteEngine.CommandsAndHandlers.Handlers.UserMetadata;
using SiteEngine.CommandsAndHandlers.Handlers.UserProfile;
using SiteEngine.CommandsAndHandlers.Handlers.OrderCommandHandlers;

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
            builder.Services.AddTransient<ICustomerUserProfileRepository, CustomerUserProfileRepository>();
            builder.Services.AddTransient<IEmployerUserProfileRepository, EmployerUserProfileRepository>();
            builder.Services.AddTransient<IOrderRepository, OrderRepository>();
            builder.Services.AddTransient<IOrderPriorityRepository, OrderPriorityRepository>();
            builder.Services.AddScoped<AuthorizeAttributeFilter>();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            builder.Services.AddMediatR(cfg =>
            {

                // �������� ��� ����������� �� ������� ������
                var assembly = Assembly.GetExecutingAssembly();

                // ������������ ��� ����������� ������ � ��������
                cfg.RegisterServicesFromAssembly(assembly);

                /*cfg.RegisterServicesFromAssemblyContaining<RegisterUserCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<LoginUserCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<GenerateTokensCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<GetUserProfilesByUserIdCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<CreateCustomerUserProfileCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<CreateEmployerUserProfileCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<UserIdMetadataCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<GetUserProfileHandler>();
                cfg.RegisterServicesFromAssemblyContaining<HasUserProfileCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<UserProfileCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<UserProfileUpdateCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<CreateOrderAndReturnOrderIdCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<GetOrderPriorityCommandHandler>();*/
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
