using API.DTOs;
using API.Entities;

namespace API.Services
{
    public interface IAuthService
    {
        Task<AppUser> Register(RegisterDto registerDto);
        Task<string> Login(LoginDto loginDto);
    }
}
