using API.Data;
using API.Entities;
using API.Interfaces;
using AutoMapper;

namespace API.Repositories
{
    public class UnitOfWork(AppDbContext context, IMapper mapper) : IUnitOfWork
    {
        public IEmployeeRepository Employees => new EmployeeRepository(context, mapper);

        public IGenericRepository<Department> Departments => new GenericRepository<Department>(context);

        public IGenericRepository<Project> Projects => new GenericRepository<Project>(context);

        public IGenericRepository<EmployeeProject> EmployeeProjects => new GenericRepository<EmployeeProject>(context);

        public int Complete()
        {
            return context.SaveChanges();
        }
    }
}
