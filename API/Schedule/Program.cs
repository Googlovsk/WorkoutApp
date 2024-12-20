
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Schedule.Controllers;
using Schedule.Data;
using Schedule.Extentions;
using Schedule.Models;
using Schedule.Models.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Schedule
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services
                .AddSwaggerExplorer()
                .InjectDbContext(builder.Configuration)
                .AddAppConfig(builder.Configuration)
                .AddIdentityHandlerAndStores()
                .ConfigureIdentityOptions()
                .AddIdentityAuth(builder.Configuration);
            
                  
            var app = builder.Build();

            app.ConfigureSwaggerExplorer()
                .ConfigureCORS(builder.Configuration)
                .AddIdentityAuthMiddlewares();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.MapControllers();
            app.MapGroup("/api").MapIdentityApi<User>();
            app.MapGroup("/api").MapIdentityUserEndpoints();
            app.Run();

        }

    }
}

