using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Salary { get; set; }
        public string? JobTitle { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public List<EmployeeProject> EmployeeProjects { get; set; } = [];

        public ICollection<EmployeeDocument> Documents { get; set; } = [];
        public ICollection<Photo> Photos { get; set; } = [];
    }
}
