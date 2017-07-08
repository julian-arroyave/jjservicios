using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJServicios.DB.Interface;

namespace JJServicios.DB.Impl.Repositories
{
    public class GenericRepository<TEntity>
         where TEntity : class
    {
        private readonly JJServiciosEntities context;
        private readonly DbSet<TEntity> dbSet;

        public GenericRepository(JJServiciosEntities context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public void Create(TEntity entity)
        {
            dbSet.Add(entity);
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
            return await dbSet.FindAsync(keyValues);
        }
        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return dbSet.SqlQuery(query, parameters).AsQueryable();
        }
        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
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
            return dbSet;
        }
    }
}
