using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CreateEmployeeDto
    {
        public required string Name { get; set; }
        public decimal Salary { get; set; }
    }
}
