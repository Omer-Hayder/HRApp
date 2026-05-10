using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace API.Services
{
    public class AuthService(UserManager<AppUser> userManager, IMapper mapper, ITokenService tokenService) : IAuthService
    {
        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.UserName);

            if (user == null)
            {
                throw new Exception("Invalid Username");
            }

            var isValidPassword = userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isValidPassword.IsCompleted)
            {
                throw new Exception("Invalid Password");
            }

            var token = await tokenService.CreateTokenAsync(user);
            return token.ToString();
        }

        public async Task<AppUser> Register(RegisterDto registerDto)
        {
            var userExist = userManager.FindByNameAsync(registerDto.UserName);

            if (userExist.IsCompletedSuccessfully)
            {
                throw new Exception("Username already exist");
            }

            var user = mapper.Map<AppUser>(registerDto);

            var result = userManager.CreateAsync(user, registerDto.Password);

            if (!result.Result.Succeeded)
            {
                foreach (var error in result.Result.Errors)
                {
                    Console.WriteLine(error);
                }
                throw new Exception("Cannot create account, please try again later");
            }

            var addRoleResult = await userManager.AddToRoleAsync(user, "User");
            if (addRoleResult.Succeeded)
            {
            }

            return user;
        }
    }
}
