namespace API.DTOs
{
    public class UploadMultiDocumentDto
    {
        public IFormFileCollection Files { get; set; } = default!;
        public int EmployeeId { get; set; }
    }
}
