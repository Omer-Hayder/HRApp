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

        public List<int> ProjectIds { get; set; } = [];

    }
}
