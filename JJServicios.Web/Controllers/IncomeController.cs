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
﻿using JJServicios.Web.Models;

namespace JJServicios.Web.Controllers
{
    public class IncomeController : Controller
    {
        private JJServiciosEntities db = new JJServiciosEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Income_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Income> income = db.Income;
            DataSourceResult result = income.ToDataSourceResult(request, c => new IncomeExpenseViewModel
            {
                Amount = c.Amount,
                Observations = c.Observations,
                MovementType = c.MovementType.Name,
                Id = c.Id,
                MovementTypeId = c.MovementTypeId,
                CreatedDate = c.CreatedDate,
                UpdateDate = c.UpdateDate
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Income_Create([DataSourceRequest]DataSourceRequest request, IncomeExpenseViewModel income)
        {
            if (ModelState.IsValid)
            {
                var entity = new Income
                {
                    Amount = income.Amount,
                    Observations = income.Observations,
                    CreatedDate = income.CreatedDate,
                    UpdateDate = income.UpdateDate,
                    MovementTypeId = income.MovementTypeId
                };

                db.Income.Add(entity);
                db.SaveChanges();
                income.Id = entity.Id;
            }

            return Json(new[] { income }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Income_Update([DataSourceRequest]DataSourceRequest request, IncomeExpenseViewModel income)
        {
            if (ModelState.IsValid)
            {
                var entity = new Income
                {
                    Id = income.Id,
                    Amount = income.Amount,
                    Observations = income.Observations,
                    CreatedDate = income.CreatedDate,
                    UpdateDate = income.UpdateDate,
                    MovementTypeId = income.MovementTypeId
                };

                db.Income.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { income }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Income_Destroy([DataSourceRequest]DataSourceRequest request, IncomeExpenseViewModel income)
        {
            if (ModelState.IsValid)
            {
                var entity = new Income
                {
                    Id = income.Id,
                    Amount = income.Amount,
                    Observations = income.Observations,
                    CreatedDate = income.CreatedDate,
                    UpdateDate = income.UpdateDate,
                    MovementTypeId = income.MovementTypeId
                };

                db.Income.Attach(entity);
                db.Income.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { income }.ToDataSourceResult(request, ModelState));
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
