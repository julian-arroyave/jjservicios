﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using JJServicios.DB.Contracts;
﻿using JJServicios.Web.Models;
﻿using Kendo.Mvc;

namespace JJServicios.Web.Controllers
{
    public class ServiceMovementController : Controller
    {
        private readonly JJServiciosEntities _db = new JJServiciosEntities();

        public ActionResult Index()
        {
            IQueryable<MovementType> movementTypes = _db.MovementType;
            ViewData["MovementTypes"] = new SelectList(movementTypes, "Id", "Name");
            IQueryable<Employee> employees = _db.Employee;
            var basicEmployees = employees.Select(x => new { x.Id, Name = x.Name});
            ViewData["Employees"] = new SelectList(basicEmployees, "Id", "Name");
            return View();
        }

        public ActionResult ServiceMovement_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<ServiceMovement> servicemovement = _db.ServiceMovement;

            MappAllViewFields(request);

            DataSourceResult result = servicemovement.ToDataSourceResult(request, serviceMovement => new ServiceMovementViewModel
            {
                Id = serviceMovement.Id,
                City = serviceMovement.City,
                STFRequirement = serviceMovement.STFRequirement,
                Amount = serviceMovement.Amount,
                Observations = serviceMovement.Observations,
                Support = serviceMovement.Support,
                CreatedDate = serviceMovement.CreatedDate,
                UpdateDate = serviceMovement.UpdateDate,
                Employee = serviceMovement.Employee.Name,
                EmployeeId = serviceMovement.EmployeeId,
                MovementType = serviceMovement.MovementType.Name,
                MovementTypeId = serviceMovement.MovementTypeId
                
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ServiceMovement_Create([DataSourceRequest]DataSourceRequest request, ServiceMovement serviceMovement)
        {
            if (ModelState.IsValid)
            {
                var entity = new ServiceMovement
                {
                    AgentId = 1,
                    City = serviceMovement.City,
                    STFRequirement = serviceMovement.STFRequirement,
                    Amount = serviceMovement.Amount,
                    Observations = serviceMovement.Observations,
                    Support = serviceMovement.Support,
                    CreatedDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    MovementTypeId = serviceMovement.MovementTypeId,
                    EmployeeId = serviceMovement.EmployeeId
                };

                _db.ServiceMovement.Add(entity);
                _db.SaveChanges();
                serviceMovement.Id = entity.Id;
            }

            return Json(new[] { serviceMovement }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ServiceMovement_Update([DataSourceRequest]DataSourceRequest request, ServiceMovement serviceMovement)
        {
            if (ModelState.IsValid)
            {
                var entity = new ServiceMovement
                {
                    Id = serviceMovement.Id,
                    City = serviceMovement.City,
                    STFRequirement = serviceMovement.STFRequirement,
                    Amount = serviceMovement.Amount,
                    Observations = serviceMovement.Observations,
                    Support = serviceMovement.Support,
                    CreatedDate = serviceMovement.CreatedDate,
                    UpdateDate = serviceMovement.UpdateDate,
                };

                _db.ServiceMovement.Attach(entity);
                _db.Entry(entity).State = EntityState.Modified;
                _db.SaveChanges();
            }

            return Json(new[] { serviceMovement }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ServiceMovement_Destroy([DataSourceRequest]DataSourceRequest request, ServiceMovement serviceMovement)
        {
            if (ModelState.IsValid)
            {
                var entity = new ServiceMovement
                {
                    Id = serviceMovement.Id,
                    City = serviceMovement.City,
                    STFRequirement = serviceMovement.STFRequirement,
                    Amount = serviceMovement.Amount,
                    Observations = serviceMovement.Observations,
                    Support = serviceMovement.Support,
                    CreatedDate = serviceMovement.CreatedDate,
                    UpdateDate = serviceMovement.UpdateDate,
                };

                _db.ServiceMovement.Attach(entity);
                _db.ServiceMovement.Remove(entity);
                _db.SaveChanges();
            }

            return Json(new[] { serviceMovement }.ToDataSourceResult(request, ModelState));
        }

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
                MappViewFields(f, "MovementType", "MovementType.Name");
                MappViewFields(f, "Employee", "Employee.Name");
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
            }
        }

    }
}
