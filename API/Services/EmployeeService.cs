using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Repositories;
using AutoMapper;
using FluentValidation;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace API.Services
{
    public class EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<EmployeeService> logger, IValidator<CreateEmployeeDto> createValidator) : IEmployeeService
    {
        public async Task<Employee> CreateAsync(CreateEmployeeDto dto)
        {
            logger.LogInformation("Create Employee Started.");
            Log.Logger.Information("Create Employee Started.");
            var validationResult = await createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("Validation failed");
                Log.Logger.Warning("Validation failed");
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            var employee = mapper.Map<Employee>(dto);
            unitOfWork.Employees.Create(employee);

            unitOfWork.Complete();
            logger.LogInformation("Employee created with id {Id}", employee.Id);
            Log.Logger.Information("Employee created with id {Id}", employee.Id);
            return employee;
        }
    }
}
