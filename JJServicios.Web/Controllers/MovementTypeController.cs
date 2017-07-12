using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using JJServicios.DB.Contracts;
using JJServicios.DB.Contracts.Repositories;
using JJServicios.Web.Models;

namespace JJServicios.Web.Controllers
{
    public class MovementTypeController : Controller
    {
        private JJServiciosEntities db = new JJServiciosEntities();

        private readonly IDeleteAdoRepository _dbAdoRepository;


        public MovementTypeController(IDeleteAdoRepository dbAdoRepository)
        {
            _dbAdoRepository = dbAdoRepository;
        }

        [AccessControlAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [AccessControlAttribute]
        public ActionResult MovementType_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<MovementType> movementtype = db.MovementType;
            DataSourceResult result = movementtype.ToDataSourceResult(request, movementType => new
            {
                movementType.Id,
                movementType.Name,
                CreatedDate = movementType.CreatedDate.ToLocalTime(),
                UpdateDate = movementType.UpdateDate.ToLocalTime(),
            });

            return Json(result);
        }

        [AccessControlAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MovementType_Create([DataSourceRequest]DataSourceRequest request, MovementType movementType)
        {
            if (ModelState.IsValid)
            {
                var entity = new MovementType
                {
                    AgentId = AuthenticationHelper.AuthenticationHelper.GetAgentId(),
                    Name = movementType.Name,
                    CreatedDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                };

                db.MovementType.Add(entity);
                db.SaveChanges();
                movementType.Id = entity.Id;
            }

            return Json(new[] { movementType }.ToDataSourceResult(request, ModelState));
        }

        [AccessControlAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MovementType_Update([DataSourceRequest]DataSourceRequest request, MovementType movementType)
        {
            if (ModelState.IsValid)
            {
                var entity = new MovementType
                {
                    Id = movementType.Id,
                    Name = movementType.Name,
                    CreatedDate = movementType.CreatedDate.ToUniversalTime(),
                    UpdateDate = DateTime.UtcNow,
                };

                db.MovementType.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { movementType }.ToDataSourceResult(request, ModelState));
        }

        [AccessControlAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MovementType_Destroy([DataSourceRequest]DataSourceRequest request, MovementType serviceMovement)
        {
            _dbAdoRepository.DeleteItemById(serviceMovement.Id, "MovementType");

            return Json(new[] { serviceMovement }.ToDataSourceResult(request, ModelState));
        }

        [AccessControlAttribute]
        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
