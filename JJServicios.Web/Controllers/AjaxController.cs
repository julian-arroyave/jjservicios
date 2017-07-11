using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JJServicios.DB.Contracts;

namespace JJServicios.Web.Controllers
{
    public class AjaxController : Controller
    {
        private JJServiciosEntities db = new JJServiciosEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMovementTypes()
        {
            IQueryable<MovementType> movementsTypes = db.MovementType;

            return Json(movementsTypes.Select(e => e.Name).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmployees()
        {
            IQueryable<Employee> employees = db.Employee;

            return Json(employees.Select(e => e.Name).Distinct(), JsonRequestBehavior.AllowGet);
        }
    }
}