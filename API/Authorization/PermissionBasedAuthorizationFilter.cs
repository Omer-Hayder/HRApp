using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace API.Authorization
{

    public class PermissionBasedAuthorizationFilter(AppDbContext dbContext) : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var attribute = (CheckPermissionAttribute)context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is CheckPermissionAttribute)!;
            if(attribute != null)
            {
                var claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
                if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
                {
                    context.Result = new ForbidResult();
                }
                else
                {
                    var userId = Guid.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                    var hasPermission = dbContext.UserPermissions.Any(up => up.UserId == userId && up.PermissionId == attribute.Permission);

                    if (!hasPermission)
                    {
                        context.Result = new ForbidResult();
                    }
                }
            }
        }
    }
}
