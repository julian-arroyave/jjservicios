using System;
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
    public class FinancialAccountController : Controller
    {
        private JJServiciosEntities db = new JJServiciosEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FinancialAccount_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<FinancialAccount> financialaccount = db.FinancialAccount;
            DataSourceResult result = financialaccount.ToDataSourceResult(request, financialAccount => new
            {
                Id = financialAccount.Id,
                Name = financialAccount.Name,
                CreatedDate = financialAccount.CreatedDate,
                UpdateDate = financialAccount.UpdateDate,
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FinancialAccount_Create([DataSourceRequest]DataSourceRequest request, FinancialAccount employeePosition)
        {
            if (ModelState.IsValid)
            {
                var entity = new FinancialAccount
                {
                    AgentId = 1,
                    Name = employeePosition.Name,
                    UpdateDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow
                };

                db.FinancialAccount.Add(entity);
                db.SaveChanges();
                employeePosition.Id = entity.Id;
            }

            return Json(new[] { employeePosition }.ToDataSourceResult(request, ModelState));
        }



        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FinancialAccount_Update([DataSourceRequest]DataSourceRequest request, FinancialAccount financialAccount)
        {
            var entity = new FinancialAccount
            {
                Id = financialAccount.Id,
                Name = financialAccount.Name,
                UpdateDate = DateTime.UtcNow,
                AgentId = 1,
                CreatedDate = financialAccount.CreatedDate
            };

            db.FinancialAccount.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();


            return Json(new[] { financialAccount }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FinancialAccount_Destroy([DataSourceRequest]DataSourceRequest request, FinancialAccount financialAccount)
        {
            if (ModelState.IsValid)
            {
                var entity = new FinancialAccount
                {
                    Id = financialAccount.Id,
                    Name = financialAccount.Name,
                    CreatedDate = financialAccount.CreatedDate,
                    UpdateDate = financialAccount.UpdateDate,
                };

                db.FinancialAccount.Attach(entity);
                db.FinancialAccount.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { financialAccount }.ToDataSourceResult(request, ModelState));
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
