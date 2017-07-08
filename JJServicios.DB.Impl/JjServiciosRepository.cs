using System.Collections.Generic;
using JJServicios.DB.Interface;
using JJServicios.Models;

namespace JJServicios.DB.Impl
{
    public class JjServiciosRepository : IJjServiciosRepository
    {
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            Employee employee = new Employee()
            {
                LastName = "Arroyave",
                Name = "Julián David",
                Ocupation = "Socio"

            };
            employees.Add(employee);
            return employees;
        }
    }
}
