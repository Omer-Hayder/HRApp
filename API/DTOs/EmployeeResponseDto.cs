using API.Entities;

namespace API.DTOs
{
    public class EmployeeResponseDto
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }

        public string? DepartmentName { get; set; }

    }
}
