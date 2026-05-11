using API.DTOs;
using API.Entities;

namespace API.Services
{
    public interface IAuthService
    {
        Task<AppUser> Register(RegisterDto registerDto);
        Task<AuthResponseDto> Login(LoginDto loginDto);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto);
    }
}
