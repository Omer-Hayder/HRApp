using API.Entities;

namespace API.DTOs
{
    public class EmployeeResponseDto
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }

        public string? DepartmentName { get; set; }

        public EmployeeResponseDto MapFromEntity(Employee employee)
        {
            return new EmployeeResponseDto()
            {
                Name = employee.Name,
                Salary = employee.Salary,
                DepartmentName = employee.Department!.Name
            };
        }
    }
}
