using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace JJServicios.DB.Interface.Repositories
{
    public interface IGenericRepository<TEntity>
         where TEntity : class
    {
        void Create(TEntity entity);
        void CreateRange(IEnumerable<TEntity> entities);
        Task<TEntity> FindAsync(params object[] keyValues);
        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task Delete(params object[] id);
        IQueryable<TEntity> Queryable();
    }
}
