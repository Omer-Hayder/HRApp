using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateEmployeeDto> validator) : ControllerBase
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
        public async Task<IActionResult> Create(CreateEmployeeDto dto)
        {
            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                return BadRequest(new
                {
                    errors = result.Errors.Select(e => e.ErrorMessage)
                });
            }

            var employee = mapper.Map<Employee>(dto);
            unitOfWork.Employees.Create(employee);

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
