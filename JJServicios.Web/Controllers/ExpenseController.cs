﻿using System;
﻿using System.Collections.Generic;
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
        private JJServiciosEntities db = new JJServiciosEntities();
        
        public ActionResult Index()
        {
            IQueryable<MovementType> movementTypes = db.MovementType;
            ViewData["MovementTypes"] = new SelectList(movementTypes, "Id", "Name");
            return View();
        }

        public ActionResult Expense_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Expense> expenses = db.Expense;
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

                db.Expense.Add(entity);
                db.SaveChanges();
                expense.Id = entity.Id;
            }

            return Json(new[] { expense }.ToDataSourceResult(request, ModelState));

        }

        private DataSourceResult GetExpensesDataResult(DataSourceRequest request, IQueryable<Expense> expenses)
        {
            IQueryable<MovementType> movementType = db.MovementType;

            //expenses = expenses.Where(x => x.MovementType.Name.Contains("Impuesto"));

            var rw = request.Filters.ToList();

            IList<FilterDescriptor> filters = new List<FilterDescriptor>();
            foreach (var f in rw)
            {
                var type = f.GetType();

             MapMovementTypeName(request.Filters, f);




            }

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

        private static void MapMovementTypeName(IList<IFilterDescriptor> filters, IFilterDescriptor f)
        {
            var type = f.GetType();

            if (type.Name != "FilterDescriptor")
            {
                CompositeFilterDescriptor cfd = (CompositeFilterDescriptor) f;

                foreach (var item in cfd.FilterDescriptors)
                {
                    MapMovementTypeName(cfd.FilterDescriptors, item);
                }
            }
            else
            {
                FilterDescriptor fd = (FilterDescriptor) f;
                if (fd.Member == "MovementType")
                {
                    fd.Member = "MovementType.Name";
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

                db.Expense.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
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
