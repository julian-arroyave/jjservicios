﻿using System;
using System.Collections.Generic;
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
    public class ExpenseController : Controller
    {
        private readonly JJServiciosEntities _db = new JJServiciosEntities();

        private readonly IDeleteAdoRepository _dbAdoRepository;

        public ExpenseController(IDeleteAdoRepository dbAdoRepository)
        {
            _dbAdoRepository = dbAdoRepository;
        }

        [AccessControlAttribute]
        public ActionResult Index()
        {
            IQueryable<MovementType> movementTypes = _db.MovementType;
            ViewData["MovementTypes"] = new SelectList(movementTypes, "Id", "Name");
            var bankAccounts = _db.BankAccount.ToList();
            Dictionary<string, string> bankAccountsDiciontary = bankAccounts.ToDictionary(ba => ba.Id.ToString(), ba => ba.Name);
            ViewData["bankAccountsDiciontary"] = bankAccountsDiciontary;
            return View();
        }

        [AccessControlAttribute]
        public ActionResult Expense_Read([DataSourceRequest]DataSourceRequest request, int bankAccountId)
        {
            IQueryable<Expense> expenses = _db.Expense.Where(x=> x.BankAccountId == bankAccountId);
            var result = GetExpensesDataResult(request, expenses);
            return Json(result);
        }

        [AccessControlAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Expense_Create([DataSourceRequest]DataSourceRequest request, IncomeExpenseViewModel expense)
        {
            if (ModelState.IsValid)
            {
                var entity = new Expense
                {
                    AgentId = AuthenticationHelper.AuthenticationHelper.GetAgentId(),
                    Amount = expense.Amount,
                    Observations = expense.Observations,
                    CreatedDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    MovementTypeId = expense.MovementTypeId,
                    BankAccountId = Convert.ToInt16(HttpContext.Request["bankAccountId"])
                };

                _db.Expense.Add(entity);
                _db.SaveChanges();
                expense.Id = entity.Id;
            }

            return Json(new[] { expense }.ToDataSourceResult(request, ModelState));

        }

        [AccessControlAttribute]
        private DataSourceResult GetExpensesDataResult(DataSourceRequest request, IQueryable<Expense> expenses)
        {
            IQueryable<MovementType> movementType = _db.MovementType;

            

            MappAllViewFields(request);

            DataSourceResult result = expenses.ToDataSourceResult(request, c => c.BankAccountId != null ? new IncomeExpenseViewModel
            {
                Amount = c.Amount,
                Observations = c.Observations,
                Id = c.Id,
                MovementType = movementType.Where(x => x.Id == c.MovementTypeId).Select(x => x.Name).First(),
                MovementTypeId = c.MovementTypeId,
                CreatedDate = c.CreatedDate.ToLocalTime(),
                UpdateDate = c.UpdateDate.ToLocalTime(),
                BankAccountId = c.BankAccountId.Value
            } : null);
            return result;
        }

        private static void MappAllViewFields(DataSourceRequest request)
        {
            var rw = request.Filters.ToList();
            foreach (var f in rw)
            {
                MappViewFields(f, "MovementType", "MovementType.Name");
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
                    fd.Value = ((DateTime)fd.Value).ToUniversalTime();
                }
            }
        }

        [AccessControlAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Expense_Update([DataSourceRequest]DataSourceRequest request, IncomeExpenseViewModel expense)
        {
            if (ModelState.IsValid)
            {
                var entity = new Expense
                {
                    AgentId = AuthenticationHelper.AuthenticationHelper.GetAgentId(),
                    Id = expense.Id,
                    Amount = expense.Amount,
                    Observations = expense.Observations,
                    CreatedDate = expense.CreatedDate.ToUniversalTime(),
                    UpdateDate = DateTime.UtcNow,
                    MovementTypeId = expense.MovementTypeId,
                    BankAccountId = expense.BankAccountId
                };

                _db.Expense.Attach(entity);
                _db.Entry(entity).State = EntityState.Modified;
                _db.SaveChanges();
            }

            return Json(new[] { expense }.ToDataSourceResult(request, ModelState));
        }

        [AccessControlAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Expense_Destroy([DataSourceRequest]DataSourceRequest request, IncomeExpenseViewModel serviceMovement)
        {

            _dbAdoRepository.DeleteItemById(serviceMovement.Id, "Expense");

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
    }
}
