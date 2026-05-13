using API.DTOs;

namespace API.Services
{
    public interface IWeatherService
    {
        Task<WeatherResponseDto?> GetWeatherAsync(string city);
    }
}
