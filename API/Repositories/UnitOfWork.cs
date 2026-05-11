using API.Data;
using API.Entities;
using API.Interfaces;
using API.Services;
using AutoMapper;

namespace API.Repositories
{
    public class UnitOfWork(AppDbContext context, IMapper mapper, ICacheService cache) : IUnitOfWork
    {
        public IEmployeeRepository Employees => new EmployeeRepository(context, mapper, cache);

        public IGenericRepository<Department> Departments => new GenericRepository<Department>(context);

        public IGenericRepository<Project> Projects => new GenericRepository<Project>(context);

        public IGenericRepository<EmployeeProject> EmployeeProjects => new GenericRepository<EmployeeProject>(context);

        public int Complete()
        {
            return context.SaveChanges();
        }
    }
}
