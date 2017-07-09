using System.Threading.Tasks;
using JJServicios.DB.Contracts;
using JJServicios.DB.Contracts.Repositories;

namespace JJServicios.DB.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private GenericRepository<Agent> _agentsRepository;
        private GenericRepository<Employee> _employeessRepository;

        public UnitOfWork()
        {
            _context = new JJServiciosEntities();
        }
        private readonly JJServiciosEntities _context;

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

        public GenericRepository<Employee> EmployeesRepository
        {
            get
            {
                if (_agentsRepository == null)
                {
                    _employeessRepository = new GenericRepository<Employee>(_context);
                }
                return _employeessRepository;
            }
        }



        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
