namespace API.DTOs
{
    public class AddUserPermissionDto
    {
        public required Guid UserId { get; set; }
        public int PermissionId { get; set; }
    }
}
