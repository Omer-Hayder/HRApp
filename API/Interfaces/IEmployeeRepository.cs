using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IEmployeeRepository: IGenericRepository<Employee>
    {
        Task<IEnumerable<EmployeeResponseDto>> GetAllWithDepartment();
    }
}
