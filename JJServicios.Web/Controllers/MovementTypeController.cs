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
    public class MovementTypeController : Controller
    {
        private JJServiciosEntities db = new JJServiciosEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MovementType_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<MovementType> movementtype = db.MovementType;
            DataSourceResult result = movementtype.ToDataSourceResult(request, movementType => new {
                Id = movementType.Id,
                Name = movementType.Name,
                CreatedDate = movementType.CreatedDate,
                UpdateDate = movementType.UpdateDate,
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MovementType_Create([DataSourceRequest]DataSourceRequest request, MovementType movementType)
        {
            if (ModelState.IsValid)
            {
                var entity = new MovementType
                {
                    AgentId = 1,
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MovementType_Update([DataSourceRequest]DataSourceRequest request, MovementType movementType)
        {
            if (ModelState.IsValid)
            {
                var entity = new MovementType
                {
                    Id = movementType.Id,
                    Name = movementType.Name,
                    CreatedDate = movementType.CreatedDate,
                    UpdateDate = DateTime.UtcNow,
                };

                db.MovementType.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { movementType }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MovementType_Destroy([DataSourceRequest]DataSourceRequest request, MovementType movementType)
        {
            if (ModelState.IsValid)
            {
                var entity = new MovementType
                {
                    Id = movementType.Id,
                    Name = movementType.Name,
                    CreatedDate = movementType.CreatedDate,
                    UpdateDate = movementType.UpdateDate,
                };

                db.MovementType.Attach(entity);
                db.MovementType.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { movementType }.ToDataSourceResult(request, ModelState));
        }

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
