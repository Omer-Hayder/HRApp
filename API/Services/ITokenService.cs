using API.DTOs;
using API.Entities;
using System.Security.Claims;

namespace API.Services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
