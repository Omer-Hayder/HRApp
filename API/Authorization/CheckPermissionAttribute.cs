using API.Entities;

namespace API.Authorization
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CheckPermissionAttribute(Permission permission) : Attribute
    {
        public Permission Permission { get; } = permission;
    }
}
