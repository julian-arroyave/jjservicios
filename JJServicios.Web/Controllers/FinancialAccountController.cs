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
    public class FinancialAccountController : Controller
    {
        private JJServiciosEntities db = new JJServiciosEntities();

        private readonly IDeleteAdoRepository _dbAdoRepository;

        public FinancialAccountController(IDeleteAdoRepository dbAdoRepository)
        {
            _dbAdoRepository = dbAdoRepository;
        }

        [AccessControlAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [AccessControlAttribute]
        public ActionResult FinancialAccount_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<FinancialAccount> financialaccount = db.FinancialAccount;
            DataSourceResult result = financialaccount.ToDataSourceResult(request, financialAccount => new
            {
                financialAccount.Id,
                financialAccount.Name,
                CreatedDate = financialAccount.CreatedDate.ToLocalTime(),
                UpdateDate = financialAccount.UpdateDate.ToLocalTime(),
            });

            return Json(result);
        }

        [AccessControlAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FinancialAccount_Create([DataSourceRequest]DataSourceRequest request, FinancialAccount employeePosition)
        {
            if (ModelState.IsValid)
            {
                var entity = new FinancialAccount
                {
                    AgentId = AuthenticationHelper.AuthenticationHelper.GetAgentId(),
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


        [AccessControlAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FinancialAccount_Update([DataSourceRequest]DataSourceRequest request, FinancialAccount financialAccount)
        {
            var entity = new FinancialAccount
            {
                Id = financialAccount.Id,
                Name = financialAccount.Name,
                UpdateDate = DateTime.UtcNow,
                AgentId = AuthenticationHelper.AuthenticationHelper.GetAgentId(),
                CreatedDate = financialAccount.CreatedDate.ToUniversalTime(),
            };

            db.FinancialAccount.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();


            return Json(new[] { financialAccount }.ToDataSourceResult(request, ModelState));
        }

        [AccessControlAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FinancialAccount_Destroy([DataSourceRequest]DataSourceRequest request, FinancialAccount serviceMovement)
        {
            _dbAdoRepository.DeleteItemById(serviceMovement.Id, "FinancialAccount");
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