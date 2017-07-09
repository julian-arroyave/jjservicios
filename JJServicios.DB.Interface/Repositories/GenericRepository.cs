using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace JJServicios.DB.Contracts.Repositories
{
    public class GenericRepository<TEntity>
         where TEntity : class
    {
        private readonly JJServiciosEntities _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(JJServiciosEntities context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
        }
        public void CreateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Create(entity);
            }
        }
        public async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }
        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return _dbSet.SqlQuery(query, parameters).AsQueryable();
        }
        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }
        public async Task Delete(params object[] id)
        {
            TEntity entity = await this.FindAsync(id);
            if (entity != null)
            {
                this.Delete(entity);
            }
        }
        public IQueryable<TEntity> Queryable()
        {
            return _dbSet;
        }
    }
}
