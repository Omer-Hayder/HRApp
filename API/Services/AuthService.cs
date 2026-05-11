using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System.Security.Claims;
using System.Security.Cryptography;

namespace API.Services
{
    public class AuthService(UserManager<AppUser> userManager, IMapper mapper, ITokenService tokenService) : IAuthService
    {
        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.UserName);

            if (user == null)
            {
                Log.Logger.Error("Invalid Username");
                throw new Exception("Invalid Username");
            }

            var isValidPassword = userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isValidPassword.IsCompleted)
            {
                Log.Logger.Error("Invalid Username");
                throw new Exception("Invalid Password");
            }

            var token = await tokenService.CreateTokenAsync(user);

            var refreshToken = tokenService.GenerateRefreshToken();
            if (refreshToken != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await userManager.UpdateAsync(user);
            }

            var authResponse = new AuthResponseDto()
            {
                Token = token.ToString(),
                RefreshToken = refreshToken!,
                RefreshTokenExpiration = user.RefreshTokenExpiryTime,
            };

            return authResponse;
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto)
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(dto.Token);

            if (principal is null)
            {
                return new AuthResponseDto
                {
                    Message = "Invalid token"
                };
            }

            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId!);

            if (user is null)
            {
                return new AuthResponseDto
                {
                    Message = "User not found"
                };
            }

            if (user.RefreshToken != dto.RefreshToken)
            {
                return new AuthResponseDto
                {
                    Message = "Invalid refresh token"
                };
            }

            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new AuthResponseDto
                {
                    Message = "Refresh token expired"
                };
            }

            var newJwtToken = await tokenService.CreateTokenAsync(user);
            var newRefreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await userManager.UpdateAsync(user);

            return new AuthResponseDto()
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken,
                RefreshTokenExpiration = user.RefreshTokenExpiryTime
            };

        }

        public async Task<AppUser> Register(RegisterDto registerDto)
        {
            var userExist = userManager.FindByNameAsync(registerDto.UserName);

            if (userExist.IsCompletedSuccessfully)
            {
                Log.Logger.Warning("Username already exist");
                throw new Exception("Username already exist");
            }

            var user = mapper.Map<AppUser>(registerDto);

            var result = userManager.CreateAsync(user, registerDto.Password);

            if (!result.Result.Succeeded)
            {
                foreach (var error in result.Result.Errors)
                {
                    Console.WriteLine(error);
                    Log.Logger.Error("Error {0}", error);
                }
                throw new Exception("Cannot create account, please try again later");
            }

            var addRoleResult = await userManager.AddToRoleAsync(user, "User");
            if (addRoleResult.Succeeded)
            {
                Log.Logger.Information("User created successfully");
            }

            return user;
        }

    }
}
