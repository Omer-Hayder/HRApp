using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController(IWeatherService weatherService) : ControllerBase
    {
        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            var result = await weatherService.GetWeatherAsync(city);

            return Ok(result);
        }
    }
}
