using API.DTOs;
using System.Text.Json;

namespace API.Services
{
    public class WeatherService(HttpClient httpClient, IConfiguration configuration) : IWeatherService
    {
        public async Task<WeatherResponseDto?> GetWeatherAsync(string city)
        {
            var apiKey = configuration["WeatherApi:ApiKey"];

            var response = await httpClient.GetAsync($"data/2.5/weather?q={city}&appid={apiKey}&units=metric");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<JsonElement>();

            return new WeatherResponseDto
            {
                City = result.GetProperty("name").GetString(),
                Temperature = result.GetProperty("main").GetProperty("temp").GetDecimal(),

                Description = result.GetProperty("weather")[0].GetProperty("description").GetString()
            };
        }
    }
}
