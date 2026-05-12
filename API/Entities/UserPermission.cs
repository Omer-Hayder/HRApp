namespace API.Entities
{
    public class UserPermission
    {
        public Guid UserId { get; set; }
        public ICollection<AppUser> AppUsers { get; set; } = [];

        public Permission PermissionId { get; set; }
    }
}
