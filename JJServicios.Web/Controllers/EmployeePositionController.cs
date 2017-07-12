﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using JJServicios.DB.Contracts;
﻿using JJServicios.DB.Contracts.Repositories;
﻿using JJServicios.Web.Models;

namespace JJServicios.Web.Controllers
{
    public class EmployeePositionController : Controller
    {
        private JJServiciosEntities db = new JJServiciosEntities();

        private readonly IDeleteAdoRepository _dbAdoRepository;

        public EmployeePositionController(IDeleteAdoRepository dbAdoRepository)
        {
            _dbAdoRepository = dbAdoRepository;
        }
        [AccessControlAttribute]
        public ActionResult Index()
        {
            return View();
        }
        [AccessControlAttribute]
        public ActionResult EmployeePosition_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<EmployeePosition> employeeposition = db.EmployeePosition;
            DataSourceResult result = employeeposition.ToDataSourceResult(request, employeePosition => new {
                employeePosition.Id,
                employeePosition.Name,
                CreatedDate = employeePosition.CreatedDate.ToLocalTime(),
                UpdateDate = employeePosition.UpdateDate.ToLocalTime(),
                employeePosition.AgentId

            });

            return Json(result);
        }

        [AccessControlAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmployeePosition_Create([DataSourceRequest]DataSourceRequest request, EmployeePosition employeePosition)
        {
            if (ModelState.IsValid)
            {
                var entity = new EmployeePosition
                {
                    AgentId = AuthenticationHelper.AuthenticationHelper.GetAgentId(),
                    Name = employeePosition.Name,
                    UpdateDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow
                };

                db.EmployeePosition.Add(entity);
                db.SaveChanges();
                employeePosition.Id = entity.Id;
            }

            return Json(new[] { employeePosition }.ToDataSourceResult(request, ModelState));
        }

        [AccessControlAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmployeePosition_Update([DataSourceRequest]DataSourceRequest request, EmployeePosition employeePosition)
        {
            if (ModelState.IsValid)
            {
                var entity = new EmployeePosition
                {
                    AgentId = AuthenticationHelper.AuthenticationHelper.GetAgentId(),
                    Id = employeePosition.Id,
                    Name = employeePosition.Name,
                    CreatedDate = employeePosition.CreatedDate.ToUniversalTime(),
                    UpdateDate = DateTime.UtcNow,
                };

                db.EmployeePosition.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { employeePosition }.ToDataSourceResult(request, ModelState));
        }

        [AccessControlAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmployeePosition_Destroy([DataSourceRequest]DataSourceRequest request, EmployeePosition serviceMovement)
        {
            try
            {
                _dbAdoRepository.DeleteItemById(serviceMovement.Id, "EmployeePosition");
            }
            catch (Exception ex)
            {
                var exMessage = ex.Message;
            }


            return Json(new[] { serviceMovement }.ToDataSourceResult(request, ModelState));
        }

        [AccessControlAttribute]
        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        [AccessControlAttribute]
        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
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
