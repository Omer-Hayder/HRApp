
using API.Data;
using API.Extentions;
using API.Helpers;
using API.Interfaces;
using API.Middleware;
using API.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var config = builder.Configuration;
            builder.Services.AddApplicationServices(config);
            builder.Services.AddIdentityServices(config);

            builder.Services.AddResponseCaching();

            // Add Redis Configuration
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration["Redis:ConnectionString"];
                options.InstanceName = builder.Configuration["Redis:InctanceName"];
            });

            // Add Cloudinary Configuration
            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

            // Add Serilog
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs/myapp", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            //builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

            var app = builder.Build();

            // Seeder Logic
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await db.Database.MigrateAsync();
                await DataSeeder.Seed(db, scope.ServiceProvider);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseResponseCaching();

            //app.UseSerilogRequestLogging();
            app.UseMiddleware<ExceptionMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
