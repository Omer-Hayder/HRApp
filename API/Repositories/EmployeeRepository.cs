using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EmployeeRepository(AppDbContext context) : IEmployeeRepository
    {
        public void Create(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = context.Employees
                //.Include(e => e.Department)
                .ToList();

            return employees;
        }

        public Employee? GetById(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee != null)
            {
                return employee;
            }
            return null;
        }

        public void Update(Employee employee)
        {
            context.Employees.Update(employee);
            context.SaveChanges();
        }
    }
}
