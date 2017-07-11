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
    public class TestController : Controller
    {
        private JJServiciosEntities db = new JJServiciosEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Expense_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Expense> expenses = db.Expense;
            DataSourceResult result = expenses.ToDataSourceResult(request, expense => new {
                Id = expense.Id,
                Amount = expense.Amount,
                Observations = expense.Observations,
                CreatedDate = expense.CreatedDate,
                UpdateDate = expense.UpdateDate,
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Expense_Create([DataSourceRequest]DataSourceRequest request, Expense expense)
        {
            if (ModelState.IsValid)
            {
                var entity = new Expense
                {
                    Amount = expense.Amount,
                    Observations = expense.Observations,
                    CreatedDate = expense.CreatedDate,
                    UpdateDate = expense.UpdateDate,
                };

                db.Expense.Add(entity);
                db.SaveChanges();
                expense.Id = entity.Id;
            }

            return Json(new[] { expense }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Expense_Update([DataSourceRequest]DataSourceRequest request, Expense expense)
        {
            if (ModelState.IsValid)
            {
                var entity = new Expense
                {
                    Id = expense.Id,
                    Amount = expense.Amount,
                    Observations = expense.Observations,
                    CreatedDate = expense.CreatedDate,
                    UpdateDate = expense.UpdateDate,
                };

                db.Expense.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { expense }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Expense_Destroy([DataSourceRequest]DataSourceRequest request, Expense expense)
        {
            if (ModelState.IsValid)
            {
                var entity = new Expense
                {
                    Id = expense.Id,
                    Amount = expense.Amount,
                    Observations = expense.Observations,
                    CreatedDate = expense.CreatedDate,
                    UpdateDate = expense.UpdateDate,
                };

                db.Expense.Attach(entity);
                db.Expense.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { expense }.ToDataSourceResult(request, ModelState));
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
