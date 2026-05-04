using API.Entities;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace API.DTOs
{
    public class CreateEmployeeDto
    {
        public required string Name { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }

        public List<int> Projects { get; set; } = [];

        public Employee MapToEmployee()
        {
            return new Employee()
            {
                Name = Name,
                Salary = Salary,
                DepartmentId = DepartmentId
            };
        }

        public EmployeeProject MapToEmployeeProject(Employee employee, int projectId)
        {
            return new EmployeeProject()
            {
                Employee = employee,
                ProjectId = projectId
            };
        }
    }
}
