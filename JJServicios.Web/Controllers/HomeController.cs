using System.Linq;
using System.Web.Mvc;
using JJServicios.DB.Impl;

namespace JJServicios.Web.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork _unit = new UnitOfWork();


        public HomeController()
        {
            var agents = _unit.AgentsRepository.Queryable().ToList();
            var twoAgents = agents;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}