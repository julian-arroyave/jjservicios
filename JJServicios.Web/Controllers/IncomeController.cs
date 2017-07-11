using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using JJServicios.DB.Contracts;
using JJServicios.Web.Models;
using Kendo.Mvc;

namespace JJServicios.Web.Controllers
{
    public class IncomeController : Controller
    {
        private readonly JJServiciosEntities _db = new JJServiciosEntities();

        public ActionResult Index()
        {
            IQueryable<MovementType> movementTypes = _db.MovementType;
            ViewData["MovementTypes"] = new SelectList(movementTypes, "Id", "Name");
            return View();
        }

        public ActionResult Income_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Income> incomes = _db.Income;
            var result = GetIncomesDataResult(request, incomes);
            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Income_Create([DataSourceRequest]DataSourceRequest request, IncomeExpenseViewModel income)
        {
            if (ModelState.IsValid)
            {
                var entity = new Income
                {
                    AgentId = 1,
                    Amount = income.Amount,
                    Observations = income.Observations,
                    CreatedDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    MovementTypeId = income.MovementTypeId
                };

                _db.Income.Add(entity);
                _db.SaveChanges();
                income.Id = entity.Id;
            }

            return Json(new[] { income }.ToDataSourceResult(request, ModelState));

        }

        private DataSourceResult GetIncomesDataResult(DataSourceRequest request, IQueryable<Income> incomes)
        {
            IQueryable<MovementType> movementType = _db.MovementType;

            MappAllViewFields(request);

            DataSourceResult result = incomes.ToDataSourceResult(request, c => new IncomeExpenseViewModel
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
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Income_Update([DataSourceRequest]DataSourceRequest request, IncomeExpenseViewModel income)
        {
            if (ModelState.IsValid)
            {
                var entity = new Income
                {
                    AgentId = 1,
                    Id = income.Id,
                    Amount = income.Amount,
                    Observations = income.Observations,
                    CreatedDate = income.CreatedDate,
                    UpdateDate = DateTime.UtcNow,
                    MovementTypeId = income.MovementTypeId
                };

                _db.Income.Attach(entity);
                _db.Entry(entity).State = EntityState.Modified;
                _db.SaveChanges();
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

                _db.Income.Attach(entity);
                _db.Income.Remove(entity);
                _db.SaveChanges();
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
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
