using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EmployeeRepository(AppDbContext context, IMapper mapper, ICacheService cache) : GenericRepository<Employee>(context), IEmployeeRepository
    {
        public async Task<IEnumerable<EmployeeResponseDto>> GetAllWithDepartment()
        {
            const string cacheKey = "employees";

            var cachedEmployee = await cache.GetAsync<List<EmployeeResponseDto>>(cacheKey);
            if (cachedEmployee != null)
                return cachedEmployee;

            var result = await context.Employees.Include(e => e.Department)
                .Select(e => mapper.Map<EmployeeResponseDto>(e))
                .ToListAsync();

            await cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5));
            return result;
        }
    }
}
