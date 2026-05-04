using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IEmployeeRepository: IGenericRepository<Employee>
    {
        IEnumerable<EmployeeResponseDto> GetAllWithDepartment();
    }
}
