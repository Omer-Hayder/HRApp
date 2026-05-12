namespace API.DTOs
{
    public class UploadMultiDocumentDto
    {
        public List<IFormFile> Files { get; set; } = [];
        public int EmployeeId { get; set; }
    }
}
