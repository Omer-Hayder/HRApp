using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserPermissionsController(AppDbContext dbContext, UserManager<AppUser> userManager) : ControllerBase
    {
        [HttpPost("assign-permission-to-user")]
        public async Task<IActionResult> AssignPermissionToUser(AddUserPermissionDto dto)
        {
            var user = await userManager.FindByIdAsync(dto.UserId.ToString());
            if(user == null)
                return BadRequest("Invalid UserId");

            var validPermission = Enum.IsDefined(typeof(Permission), dto.PermissionId);
            if (!validPermission)
                return BadRequest("Invalid PermissionId");

            var permissionId = (Permission)dto.PermissionId;

            var hasPermission = await dbContext.UserPermissions.AnyAsync(x => x.UserId == dto.UserId && x.PermissionId == permissionId);

            if (hasPermission)
                return Ok("The user already have permission");

            var userPermission = new UserPermission { UserId =  dto.UserId, PermissionId = permissionId };
            await dbContext.UserPermissions.AddAsync(userPermission);
            dbContext.SaveChanges();

            return Ok("Permission assigned successfully to user");
        }
    }
}
