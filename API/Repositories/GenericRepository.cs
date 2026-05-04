using API.Data;
using API.Interfaces;

namespace API.Repositories
{
    public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : class
    {
        public void Create(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Delete(int id)
        {
            var entity = context.Set<T>().Find(id);
            if(entity != null)
                context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T? GetById(int id)
        {
            var entity = context.Set<T>().Find(id);
            return entity;
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }
    }
}
