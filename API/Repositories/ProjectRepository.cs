using API.Data;
using API.Entities;
using API.Interfaces;

namespace API.Repositories
{
    public class ProjectRepository(AppDbContext context): GenericRepository<Project>(context), IProjectRepository
    {
    }
}
