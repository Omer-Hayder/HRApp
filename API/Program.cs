
using API.Data;
using API.Extentions;
using API.Interfaces;
using API.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var config = builder.Configuration;
            builder.Services.AddApplicationServices(config);

            var app = builder.Build();

            // Seeder Logic
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                DataSeeder.Seed(db);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
