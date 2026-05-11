using API.DTOs;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto dto)
        {
            var result = await authService.Login(dto);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto dto)
        {
            var resut = await authService.Register(dto);
            return Ok(resut);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken(RefreshTokenRequestDto dto)
        {
            var result = await authService.RefreshTokenAsync(dto);

            if (result.Message == null)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
