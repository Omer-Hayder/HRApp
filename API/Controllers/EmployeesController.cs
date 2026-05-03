using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(AppDbContext context) : ControllerBase
    {

        // 1️⃣ Get All
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = from emp in context.Employees
                         where emp.Salary > 4000
                         where emp.Salary < 6000
                         select emp;
            return Ok(result);
        }

        // 2️⃣ Get Permanent Employees
        [HttpGet("permanent")]
        public IActionResult GetPermanent()
        {
            var result = context.Employees.OfType<PermanentEmployee>()
                .Average(x => x.Salary);
            return Ok(result);
        }

        // 3️⃣ Get Contract Employees
        [HttpGet("contract")]
        public IActionResult GetContract()
        {
            var result = context.Employees.OfType<ContractEmployee>()
                .Average(x => x.Salary);
            return Ok(result);
        }

        // 4️⃣ Filter by Salary
        [HttpGet("salary/{min}")]
        public IActionResult GetBySalary(decimal min)
        {
            var result = context.Employees
                .Where(x => x.Salary >=min)
                .Where(x => x.Salary <= 5000)
                .ToList();
            return Ok(result);
        }

        // 5️⃣ Include Department
        [HttpGet("with-department")]
        public IActionResult WithDepartment()
        {
            var result = from emp in context.Employees
                         join dept in context.Departments
                         on emp.DepartmentId equals dept.Id
                         select new
                         {
                             EmployeeName = emp.Name,
                             DepartmentName = emp.Department!.Name
                         };
            return Ok(result);
        }

        // 6️⃣ Employees with Projects
        [HttpGet("with-projects")]
        public IActionResult WithProjects()
        {
            var result = context.Employees
                .Where(e => e.EmployeeProjects.Any())
                .ToList();
            return Ok(result);
        }

        // 7️⃣ Top 5 Employees by Hours
        [HttpGet("top-hours")]
        public IActionResult TopHours()
        {
            var result = context.EmployeeProjects
                .GroupBy(ep => ep.Employee!.Name)
                .Select(x => new
                {
                    Employee = x.Key,
                    TotalHours = x.Sum(s => s.HoursWorked)
                })
                .OrderByDescending(o => o.TotalHours)
                .Take(5).ToList();
            return Ok(result);
        }

        // 8️⃣ Average Salary
        [HttpGet("avg-salary")]
        public IActionResult AvgSalary()
        {
            var result = context.Employees.Average(x => x.Salary);
                
            return Ok(result);
        }

        // 9️⃣ Search
        [HttpGet("search")]
        public IActionResult Search(string name)
        {
            var result = context.Employees.Where(x => x.Name.StartsWith(name));
            return Ok(result);
        }

        // 10 Pagination
        [HttpGet("employee-pagination")]
        public IActionResult EmployeePagination(int pageNumber, int pageSize = 5)
        {
            var result = context.Employees
                .Skip((pageNumber -1) * pageSize)
                .Take(pageSize)
                .ToList();
            return Ok(result);
        }
    }
}
