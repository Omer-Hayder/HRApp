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
            return Ok();
        }

        // 2️⃣ Get Permanent Employees
        [HttpGet("permanent")]
        public IActionResult GetPermanent()
        {
            return Ok();
        }

        // 3️⃣ Get Contract Employees
        [HttpGet("contract")]
        public IActionResult GetContract()
        {
            return Ok();
        }

        // 4️⃣ Filter by Salary
        [HttpGet("salary/{min}")]
        public IActionResult GetBySalary(decimal min)
        {
            return Ok();
        }

        // 5️⃣ Include Department
        [HttpGet("with-department")]
        public IActionResult WithDepartment()
        {
            return Ok();
        }

        // 6️⃣ Employees with Projects
        [HttpGet("with-projects")]
        public IActionResult WithProjects()
        {
            return Ok();
        }

        // 7️⃣ Top 5 Employees by Hours
        [HttpGet("top-hours")]
        public IActionResult TopHours()
        {
            return Ok();
        }

        // 8️⃣ Average Salary
        [HttpGet("avg-salary")]
        public IActionResult AvgSalary()
        {
            return Ok();
        }

        // 9️⃣ Search
        [HttpGet("search")]
        public IActionResult Search(string name)
        {
            return Ok();
        }
    }
}
