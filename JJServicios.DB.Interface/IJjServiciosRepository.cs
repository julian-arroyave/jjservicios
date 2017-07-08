using System.Collections.Generic;
using JJServicios.Models;

namespace JJServicios.DB.Interface
{
    public interface IJjServiciosRepository
    {
        List<Employee> GetEmployees();
    }
}