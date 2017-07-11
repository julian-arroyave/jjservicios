﻿using System;
﻿using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using JJServicios.DB.Contracts;
﻿using JJServicios.Web.Models;
﻿using Kendo.Mvc;

namespace JJServicios.Web.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly JJServiciosEntities _db = new JJServiciosEntities();
        
        public ActionResult Index()
        {
            IQueryable<MovementType> movementTypes = _db.MovementType;
            ViewData["MovementTypes"] = new SelectList(movementTypes, "Id", "Name");
            return View();
        }

        public ActionResult Expense_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Expense> expenses = _db.Expense;
            var result = GetExpensesDataResult(request, expenses);
            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Expense_Create([DataSourceRequest]DataSourceRequest request, IncomeExpenseViewModel expense)
        {
            if (ModelState.IsValid)
            {
                var entity = new Expense
                {
                    AgentId = 1,
                    Amount = expense.Amount,
                    Observations = expense.Observations,
                    CreatedDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    MovementTypeId = expense.MovementTypeId
                };

                _db.Expense.Add(entity);
                _db.SaveChanges();
                expense.Id = entity.Id;
            }

            return Json(new[] { expense }.ToDataSourceResult(request, ModelState));

        }

        private DataSourceResult GetExpensesDataResult(DataSourceRequest request, IQueryable<Expense> expenses)
        {
            IQueryable<MovementType> movementType = _db.MovementType;

            MappAllViewFields(request);

            DataSourceResult result = expenses.ToDataSourceResult(request, c => new IncomeExpenseViewModel
            {
                Amount = c.Amount,
                Observations = c.Observations,
                Id = c.Id,
                MovementType = movementType.Where(x => x.Id == c.MovementTypeId).Select(x => x.Name).First(),
                MovementTypeId = c.MovementTypeId,
                CreatedDate = c.CreatedDate.ToLocalTime(),
                UpdateDate = c.UpdateDate.ToLocalTime()
            });
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
                CompositeFilterDescriptor cfd = (CompositeFilterDescriptor) f;

                foreach (var item in cfd.FilterDescriptors)
                {
                    MappViewFields(item,current,toMap);
                }
            }
            else
            {
                FilterDescriptor fd = (FilterDescriptor) f;
                if (fd.Member == current)
                {
                    fd.Member = toMap;
                }
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Expense_Update([DataSourceRequest]DataSourceRequest request, IncomeExpenseViewModel expense)
        {
            if (ModelState.IsValid)
            {
                var entity = new Expense
                {
                    AgentId = 1,
                    Id = expense.Id,
                    Amount = expense.Amount,
                    Observations = expense.Observations,
                    CreatedDate = expense.CreatedDate,
                    UpdateDate = DateTime.UtcNow,
                    MovementTypeId = expense.MovementTypeId
                };

                _db.Expense.Attach(entity);
                _db.Entry(entity).State = EntityState.Modified;
                _db.SaveChanges();
            }

            return Json(new[] { expense }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Expense_Destroy([DataSourceRequest]DataSourceRequest request, IncomeExpenseViewModel expense)
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
                    MovementTypeId = expense.MovementTypeId
                };

                _db.Expense.Attach(entity);
                _db.Expense.Remove(entity);
                _db.SaveChanges();
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
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
