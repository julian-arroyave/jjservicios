using System.Linq;
using System.Web.Mvc;
using JJServicios.DB.Contracts;
using JJServicios.Web.Models;

namespace JJServicios.Web.Controllers
{
    public class AjaxController : Controller
    {
        private readonly JJServiciosEntities _db = new JJServiciosEntities();

        public ActionResult Index()
        {
            return View();
        }

        [AccessControlAttribute]
        public ActionResult GetMovementTypes()
        {
            IQueryable<MovementType> movementsTypes = _db.MovementType;

            return Json(movementsTypes.Select(e => e.Name).Distinct(), JsonRequestBehavior.AllowGet);
        }
        [AccessControlAttribute]
        public ActionResult GetEmployees()
        {
            IQueryable<Employee> employees = _db.Employee;

            return Json(employees.Select(e => e.Name).Distinct(), JsonRequestBehavior.AllowGet);
        }

        [AccessControlAttribute]
        public ActionResult GetEmployeePositions()
        {
            IQueryable<EmployeePosition> employeePosition = _db.EmployeePosition;

            return Json(employeePosition.Select(e => e.Name).Distinct(), JsonRequestBehavior.AllowGet);
        }

        [AccessControlAttribute]
        public ActionResult GetFincancialAccounts()
        {
            IQueryable<FinancialAccount> financialAccount = _db.FinancialAccount;

            return Json(financialAccount.Select(e => e.Name).Distinct(), JsonRequestBehavior.AllowGet);
        }
    }
}