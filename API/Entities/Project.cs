namespace API.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public List<EmployeeProject> EmployeeProjects { get; set; } = [];
    }
}
