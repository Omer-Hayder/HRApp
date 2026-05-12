namespace API.DTOs
{
    public class UploadDocumentDto
    {
        public required IFormFile File { get; set; }
        public int EmployeeId { get; set; }
    }
}
