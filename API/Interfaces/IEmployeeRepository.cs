using API.Entities;

namespace API.Interfaces
{
    public interface IEmployeeRepository: IGenericRepository<Employee>
    {
        IEnumerable<Employee> GetAllWithDepartment();
    }
}
