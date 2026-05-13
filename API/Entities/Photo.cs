namespace API.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public required string Url { get; set; }
        public string? PublicId { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
