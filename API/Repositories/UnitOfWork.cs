using API.Data;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace API.Repositories
{
    public class UnitOfWork(AppDbContext context, IMapper mapper, IMemoryCache cache) : IUnitOfWork
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
