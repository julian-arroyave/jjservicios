using System.Linq;
using System.Web.Mvc;
using JJServicios.DB.Contracts;
using JJServicios.Web.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace JJServicios.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private JJServiciosEntities db = new JJServiciosEntities();

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Agent_Read([DataSourceRequest]DataSourceRequest request)
        {
            var agents = _unitOfWork.AgentsRepository.Queryable();

            var agentlist = agents.Select(x => new {x.Password, x.Name, x.Id, x.Email}).ToList();

            var agentsViewModel =
                agentlist.Select(x => new AgentViewModel() {Name = x.Name, Email = x.Email, Password = x.Password, Id = x.Id}).ToList();

            return Json(agentsViewModel.ToDataSourceResult(request));
        }

        public ActionResult GetMovementTypes()
        {
            IQueryable<MovementType> movementsTypes = db.MovementType;

            return Json(movementsTypes.Select(e => e.Name).Distinct(), JsonRequestBehavior.AllowGet);
        }

    }
}