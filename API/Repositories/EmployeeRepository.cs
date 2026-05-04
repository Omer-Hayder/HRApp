using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EmployeeRepository(AppDbContext context) : GenericRepository<Employee>(context), IEmployeeRepository
    {
        public IEnumerable<Employee> GetAllWithDepartment()
        {
            return context.Employees.Include(e => e.Department).ToList();
        }
    }
}
