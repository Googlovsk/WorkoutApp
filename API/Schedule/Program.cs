using AuthECAPI.Extensions;
using Schedule.Controllers;
using Schedule.Extentions;
using Schedule.Models.Domain;

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
                .AddIdentityHandlersAndStores()
                .ConfigureIdentityOptions()
                .AddIdentityAuth(builder.Configuration);

            var app = builder.Build();
            app.ConfigureSwaggerExplorer()
                .ConfigureCORS(builder.Configuration)
                .AddIdentityAuthMiddlewares();

            //app.UseHttpsRedirection();
            //app.UseRouting();

            app.MapControllers();
            app.MapGroup("/api").MapIdentityApi<AppUser>();
            app.MapGroup("/api")
               .MapIdentityUserEndpoints()
               .MapAccountEndpoints();
            app.Run();

        }

    }
}

