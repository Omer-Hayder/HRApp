using API.Exceptions;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController(IWeatherService weatherService) : ControllerBase
    {
        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            try
            {
                var result = await weatherService.GetWeatherAsync(city);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}
