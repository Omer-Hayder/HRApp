using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EmployeeRepository(AppDbContext context) : GenericRepository<Employee>(context), IEmployeeRepository
    {
        public IEnumerable<EmployeeResponseDto> GetAllWithDepartment()
        {
            var result = context.Employees.Include(e => e.Department)
                .Select(e => new EmployeeResponseDto().MapFromEntity(e))
                .ToList();
            return result;
        }
    }
}
