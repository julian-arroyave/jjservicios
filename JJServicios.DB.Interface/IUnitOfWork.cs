using JJServicios.DB.Contracts.Repositories;

namespace JJServicios.DB.Contracts
{
    public interface IUnitOfWork
    {
        GenericRepository<Agent> AgentsRepository { get; }
        GenericRepository<Employee> EmployeesRepository { get; }
    }
}
