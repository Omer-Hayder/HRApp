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

        [HttpGet]
        public IActionResult Get()
        {
            var employees = context.Employees
                .Include(e => e.Department)
                .ToList();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();

            return Ok(employee);
        }

        [HttpPut]
        public IActionResult Update(Employee employee)
        {
            context.Employees.Update(employee);
            context.SaveChanges();

            return Ok(employee);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee == null)
                return NotFound();

            context.Employees.Remove(employee);
            context.SaveChanges();
            return Ok();
        }
    }
}
