namespace API.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }

        public List<Employee> Employees { get; set; } = [];
    }
}
