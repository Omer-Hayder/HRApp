using API.Data;
using API.Entities;
using API.Interfaces;
using API.Mappings;
using API.Repositories;
using API.Services;
using API.Validators;
using Asp.Versioning;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Polly;
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

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            // Add HttpClient 
            services.AddHttpClient<IWeatherService, WeatherService>(client =>
            {
                client.BaseAddress = new Uri("https://api.openweathermap.org/");
                client.Timeout = TimeSpan.FromSeconds(10);
            }).AddTransientHttpErrorPolicy(policy =>
                policy.WaitAndRetryAsync(3, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (response, delay, retryCount, context) =>
                    {
                        Console.WriteLine($"Retry Attempt: {retryCount}");
                    }
                )
             ).AddTransientHttpErrorPolicy(policy =>
                policy.CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromSeconds(30))
             );


            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICacheService, CacheService>();

            services.AddAutoMapper(typeof(MappingProfile));

            //services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateEmployeeValidator>();


            return services;
        }
    }
}
