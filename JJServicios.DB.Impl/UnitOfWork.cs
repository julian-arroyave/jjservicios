using JJServicios.DB.Interface;
using JJServicios.DB.Impl.Repositories;
using System.Threading.Tasks;

namespace JJServicios.DB.Impl
{
    public class UnitOfWork
    {
        public UnitOfWork()
        {
            _context = new JJServiciosEntities();
        }
        private readonly JJServiciosEntities _context;

        private GenericRepository<Agent> _agentsRepository;
        public GenericRepository<Agent> AgentsRepository
        {
            get
            {
                if (_agentsRepository == null)
                {
                    _agentsRepository = new GenericRepository<Agent>(_context);
                }
                return _agentsRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
