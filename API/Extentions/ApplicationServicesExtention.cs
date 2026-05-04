using API.Data;
using API.Interfaces;
using API.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace API.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers()
                .AddJsonOptions(opt =>
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            services.AddOpenApi();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
