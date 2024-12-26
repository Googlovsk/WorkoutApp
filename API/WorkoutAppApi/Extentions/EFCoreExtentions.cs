using Microsoft.EntityFrameworkCore;
using WorkoutAppApi.Data;

namespace WorkoutAppApi.Extentions
{
    public static class EFCoreExtentions
    {
        public static IServiceCollection InjectDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConntection")));
            return services;
        }
    }
}
