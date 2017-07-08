using System.Web.Mvc;
using JJServicios.DB.Interface;

namespace JJServicios.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJjServiciosRepository _jjServiciosDbRepository;

        public HomeController(IJjServiciosRepository  jjServiciosDbRepository)
        {
             _jjServiciosDbRepository = jjServiciosDbRepository;
        }

        public ActionResult Index()
        {
            return View(_jjServiciosDbRepository.GetEmployees());
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