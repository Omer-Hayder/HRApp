using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EmployeeRepository(AppDbContext context, IMapper mapper) : GenericRepository<Employee>(context), IEmployeeRepository
    {
        public IEnumerable<EmployeeResponseDto> GetAllWithDepartment()
        {
            var result = context.Employees.Include(e => e.Department)
                .Select(e => mapper.Map<EmployeeResponseDto>(e))
                .ToList();
            return result;
        }
    }
}
