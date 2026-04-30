using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public abstract class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Salary { get; set; }
        public string? JobTitle { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public List<EmployeeProject> EmployeeProjects { get; set; } = [];
    }
}
