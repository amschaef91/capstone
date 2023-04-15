using Microsoft.EntityFrameworkCore;
using PersonalProject.Data;
using PersonalProject.Models.Configuration;

namespace PersonalProject.Models.Repositories
{
    public class ItemRepository<T> : IItemRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        private DbSet<T> dbset { get; set; }

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
            dbset = _context.Set<T>();
        }

        public virtual IEnumerable<T> List(QueryOptions<T> options) =>
            BuildQuery(options).ToList();
        public int Count => dbset.Count();
        public virtual T? Get(int id) => dbset.Find(id);
        public virtual T? Get(string id) => dbset.Find(id);
        public virtual T? Get(QueryOptions<T> options) => 
            BuildQuery(options).FirstOrDefault();


        public virtual void Insert(T entity) => dbset.Add(entity);
        public virtual void Update(T entity) => dbset.Update(entity);
        public virtual void Delete(T entity) => dbset.Remove(entity);
        public virtual void Save() => _context.SaveChanges();

        private IQueryable<T> BuildQuery(QueryOptions<T> options)
        {
            IQueryable<T> query = dbset;
            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }
            if (options.HasWhere)
            {
                query = query.Where(options.Where);
            }
            if (options.HasOrderBy)
            {
                if (options.OrderByDirection == "asc")
                    query = query.OrderBy(options.OrderBy);
                else
                    query = query.OrderByDescending(options.OrderBy);
            }

            return query;
        }
    }
}
