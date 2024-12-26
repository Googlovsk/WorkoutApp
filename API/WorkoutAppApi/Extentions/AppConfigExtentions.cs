﻿using Microsoft.EntityFrameworkCore;
using WorkoutAppApi.Data;
using WorkoutAppApi.Models;

namespace WorkoutAppApi.Extentions
{
    public static class AppConfigExtentions
    {
        public static WebApplication ConfigureCORS(this WebApplication app, IConfiguration config)
        {
            app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
            return app;
        }
        public static IServiceCollection AddAppConfig(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AppSettings>(config.GetSection("AppSettings"));
            return services;
        }
    }
}
