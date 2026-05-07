using API.DTOs;
using API.Entities;

namespace API.Services
{
    public interface IEmployeeService
    {
        Task<Employee> CreateAsync(CreateEmployeeDto dto);
    }
}
