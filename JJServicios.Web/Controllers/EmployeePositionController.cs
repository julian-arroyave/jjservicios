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

namespace JJServicios.Web.Controllers
{
    public class EmployeePositionController : Controller
    {
        private JJServiciosEntities db = new JJServiciosEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmployeePosition_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<EmployeePosition> employeeposition = db.EmployeePosition;
            DataSourceResult result = employeeposition.ToDataSourceResult(request, employeePosition => new {
                Id = employeePosition.Id,
                Name = employeePosition.Name,
                CreatedDate = employeePosition.CreatedDate,
                UpdateDate = employeePosition.UpdateDate,
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmployeePosition_Create([DataSourceRequest]DataSourceRequest request, EmployeePosition employeePosition)
        {
            if (ModelState.IsValid)
            {
                var entity = new EmployeePosition
                {
                    AgentId = 1,
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmployeePosition_Update([DataSourceRequest]DataSourceRequest request, EmployeePosition employeePosition)
        {
            if (ModelState.IsValid)
            {
                var entity = new EmployeePosition
                {
                    Id = employeePosition.Id,
                    Name = employeePosition.Name,
                    CreatedDate = employeePosition.CreatedDate,
                    UpdateDate = employeePosition.UpdateDate,
                };

                db.EmployeePosition.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { employeePosition }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmployeePosition_Destroy([DataSourceRequest]DataSourceRequest request, EmployeePosition employeePosition)
        {
            if (ModelState.IsValid)
            {
                var entity = new EmployeePosition
                {
                    Id = employeePosition.Id,
                    Name = employeePosition.Name,
                    CreatedDate = employeePosition.CreatedDate,
                    UpdateDate = employeePosition.UpdateDate,
                };

                db.EmployeePosition.Attach(entity);
                db.EmployeePosition.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { employeePosition }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    
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
