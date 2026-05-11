using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace API.Repositories
{
    public class EmployeeRepository(AppDbContext context, IMapper mapper, IMemoryCache cache) : GenericRepository<Employee>(context), IEmployeeRepository
    {
        public async Task<IEnumerable<EmployeeResponseDto>> GetAllWithDepartment()
        {
            if(!cache.TryGetValue("employees", out List<EmployeeResponseDto> employees))
            {
                employees = await context.Employees.Include(e => e.Department)
                .Select(e => mapper.Map<EmployeeResponseDto>(e))
                .ToListAsync();

                cache.Set("employees", employees, TimeSpan.FromMinutes(5));
            }       
            return employees;
        }
    }
}
