using API.Entities;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employees { get; }
        IGenericRepository<Department> Departments { get; }
        IGenericRepository<Project> Projects { get; }
        IGenericRepository<EmployeeProject> EmployeeProjects { get; }

        int Complete();
    }
}
