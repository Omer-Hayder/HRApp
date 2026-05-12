namespace API.Entities
{
    public class EmployeeDocument
    {
        public int Id { get; set; }
        public required string FileName { get; set; }
        public required string OriginalName { get; set; }
        public required string ContentType { get; set; }
        public long Size { get; set; }
        public DateTime UploadDate { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
