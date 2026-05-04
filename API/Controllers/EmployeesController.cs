using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IUnitOfWork unitOfWork) : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(unitOfWork.Employees.GetAllWithDepartment());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(unitOfWork.Employees.GetById(id));
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto dto)
        {
            var employee = dto.MapToEmployee();
            unitOfWork.Employees.Create(employee);

            foreach (var project in dto.Projects)
            {
                unitOfWork.EmployeeProjects.Create(dto.MapToEmployeeProject(employee, project));
            }

            unitOfWork.Complete();
            return Ok(employee);
        }

        [HttpPut]
        public IActionResult Update(Employee employee)
        {
            unitOfWork.Employees.Update(employee);
            unitOfWork.Complete();
            return Ok(employee);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            unitOfWork.Employees.Delete(id);
            unitOfWork.Complete();
            return Ok();
        }
    }
}
