using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using JJServicios.DB.Contracts;
using JJServicios.DB.Contracts.Repositories;
using JJServicios.Web.Models;
using Kendo.Mvc;

namespace JJServicios.Web.Controllers
{
    public class WorkTimeLogController : Controller
    {
        private readonly JJServiciosEntities _db = new JJServiciosEntities();

        private readonly IDeleteAdoRepository _dbAdoRepository;

        public WorkTimeLogController(IDeleteAdoRepository dbAdoRepository)
        {
            _dbAdoRepository = dbAdoRepository;
        }

        [AccessControlAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [AccessControlAttribute]
        public ActionResult WorkTimeLog_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<WorkTimeLog> movementtype = _db.WorkTimeLog;

            MappAllViewFields(request);

            DataSourceResult result = movementtype.ToDataSourceResult(request, movementType => new
            {
                movementType.Id,
                movementType.Observations,
                Agent = movementType.Agent.Name,
                CreatedDate = movementType.CreatedDate.ToLocalTime(),
                UpdateDate = movementType.UpdateDate.ToLocalTime(),
                AgentId = AuthenticationHelper.AuthenticationHelper.GetAgentId(),
                StartDate = movementType.StartDate.ToLocalTime(),
                EndDate =movementType.EndDate.ToLocalTime(),
                WorkedHours = string.Concat((movementType.EndDate - movementType.StartDate).Hours,"h ", (movementType.EndDate - movementType.StartDate).Minutes,"m")
            });

            return Json(result);
        }

        [AccessControlAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult WorkTimeLog_Create([DataSourceRequest]DataSourceRequest request, WorkTimeLogViewModel movementType)
        {
            if (ModelState.IsValid)
            {
                var entity = new WorkTimeLog
                {
                Id =  movementType.Id,
                Observations = movementType.Observations,
                CreatedDate = DateTime.UtcNow,
                UpdateDate =  DateTime.UtcNow,
                AgentId = AuthenticationHelper.AuthenticationHelper.GetAgentId(),
                StartDate = movementType.StartDate.ToUniversalTime(),
                EndDate =movementType.EndDate.ToUniversalTime(),
                };

                _db.WorkTimeLog.Add(entity);
                _db.SaveChanges();
                movementType.Id = entity.Id;
            }

            return Json(new[] { movementType }.ToDataSourceResult(request, ModelState));
        }

        [AccessControlAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult WorkTimeLog_Update([DataSourceRequest]DataSourceRequest request, WorkTimeLogViewModel movementType)
        {
            if (ModelState.IsValid)
            {
                var entity = new WorkTimeLog
                {
                    Id = movementType.Id,
                    Observations = movementType.Observations,
                    CreatedDate = movementType.CreatedDate.ToUniversalTime(),
                    UpdateDate = DateTime.UtcNow,
                    AgentId = AuthenticationHelper.AuthenticationHelper.GetAgentId(),
                    StartDate = movementType.StartDate.ToUniversalTime(),
                    EndDate = movementType.EndDate.ToUniversalTime(),
                };
                _db.WorkTimeLog.Attach(entity);
                _db.Entry(entity).State = EntityState.Modified;
                _db.SaveChanges();
            }

            return Json(new[] { movementType }.ToDataSourceResult(request, ModelState));
        }

        [AccessControlAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MWorkTimeLog_Destroy([DataSourceRequest]DataSourceRequest request, WorkTimeLogViewModel serviceMovement)
        {
            _dbAdoRepository.DeleteItemById(serviceMovement.Id, "WorkTimeLog");

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
            _db.Dispose();
            base.Dispose(disposing);
        }

        private static void MappAllViewFields(DataSourceRequest request)
        {
            var rw = request.Filters.ToList();
            foreach (var f in rw)
            {
                MappViewFields(f, "Agent", "Agent.Name");
            }
        }

        private static void MappViewFields(IFilterDescriptor f, string current, string toMap)
        {
            var type = f.GetType();

            if (type.Name != "FilterDescriptor")
            {
                CompositeFilterDescriptor cfd = (CompositeFilterDescriptor)f;

                foreach (var item in cfd.FilterDescriptors)
                {
                    MappViewFields(item, current, toMap);
                }
            }
            else
            {
                FilterDescriptor fd = (FilterDescriptor)f;
                if (fd.Member == current)
                {
                    fd.Member = toMap;
                }
                if (fd.Member.ToLower().Contains("date"))
                {
                    fd.Value =   ((DateTime)fd.Value).ToUniversalTime();
                }
                
            }
        }
    }
}
