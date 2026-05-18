using API.DTOs;
using API.Exceptions;
using System.Net;
using System.Text.Json;

namespace API.Services
{
    public class WeatherService(HttpClient httpClient, IConfiguration configuration, ILogger<WeatherService> logger) : IWeatherService
    {
        public async Task<WeatherResponseDto?> GetWeatherAsync(string city)
        {
            try
            {
                logger.LogInformation("Fetching weather for city: {City}", city);

                var apiKey = configuration["WeatherApi:ApiKey"];
                var response = await httpClient.GetAsync($"data/2.5/weathers?q={city}&appid={apiKey}&units=metric");

                if (!response.IsSuccessStatusCode)
                {
                    logger.LogWarning("Weather API returned {StatusCode} for city: {City}", response.StatusCode, city);

                    if (response.StatusCode == HttpStatusCode.NotFound)
                        return null;
                    if (response.StatusCode == HttpStatusCode.TooManyRequests)
                        throw new HttpRequestException("Rate limited by weather service");

                    response.EnsureSuccessStatusCode();
                }


                var result = await response.Content.ReadFromJsonAsync<JsonElement>();

                return new WeatherResponseDto
                {
                    City = result.GetProperty("name").GetString(),
                    Temperature = result.GetProperty("main").GetProperty("temp").GetDecimal(),

                    Description = result.GetProperty("weather")[0].GetProperty("description").GetString()
                };
            }
            catch (TaskCanceledException)
            {
                logger.LogError("Weather API request timed out for city: {City}", city);
                throw new HttpRequestException("Weather service timed out. Please try again.");
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error fetching weather for city: {City}", city);
                throw new HttpRequestException("An unexpected error occurred while fetching weather data.");
            }
        }
    }
}
