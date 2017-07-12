using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JJServicios.DB.Contracts;
using JJServicios.Web.Models;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;

namespace JJServicios.Web.Controllers
{
    public class SummaryController : Controller
    {
        private readonly JJServiciosEntities _db = new JJServiciosEntities();
        private static DateTime? _upperLimitDate;
        private static DateTime? _lowerLimitDate;

        public SummaryController()
        {

            var bankAccounts = _db.BankAccount.ToList();
            Dictionary<string, string> bankAccountsDiciontary = bankAccounts.ToDictionary(ba => ba.Id.ToString(), ba => ba.Name);
            ViewData["bankAccountsDiciontary"] = bankAccountsDiciontary;
        }

        [AccessControlAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [AccessControlAttribute]
        public ActionResult Agent_Read([DataSourceRequest]DataSourceRequest request, int bankAccountId)
        {
            MappAllViewFields(request);

            DateTime lowerDate = DateTime.UtcNow.AddDays(-60); 
            DateTime upperDate = DateTime.UtcNow;

            if (_lowerLimitDate.HasValue)
            {
                lowerDate = _lowerLimitDate.Value;
            }

            if (_upperLimitDate.HasValue)
            {
                upperDate = _upperLimitDate.Value;
            }

            IQueryable<Expense> expenses = _db.Expense.Where(x => x.CreatedDate >= lowerDate && x.CreatedDate <= upperDate && x.BankAccountId == bankAccountId);
            IQueryable<Income> incomes = _db.Income.Where(x => x.CreatedDate >= lowerDate && x.CreatedDate <= upperDate && x.BankAccountId == bankAccountId);
            IQueryable<ServiceMovement> serviceMovement = _db.ServiceMovement.Where(x => x.CreatedDate >= lowerDate && x.CreatedDate <= upperDate && x.BankAccountId == bankAccountId);


            IQueryable<Expense> prevExpenses = _db.Expense.Where(x => x.CreatedDate < lowerDate && x.BankAccountId == bankAccountId);
            IQueryable<Income> prevIncomes = _db.Income.Where(x => x.CreatedDate < lowerDate && x.BankAccountId == bankAccountId);
            IQueryable<ServiceMovement> prevServiceMovement = _db.ServiceMovement.Where(x => x.CreatedDate < lowerDate && x.BankAccountId == bankAccountId);

            var prevTotal = prevIncomes.ToList().Sum(x => x.Amount) - prevExpenses.ToList().Sum(x => x.Amount) -
                            prevServiceMovement.ToList().Sum(x => x.Amount);

            IList<SummaryViewModel> summaryList = new List<SummaryViewModel>();

            summaryList.AddRange(expenses.Select(x => new SummaryViewModel()
            {
                AmountOut = x.Amount,
                AgentName = x.Agent.Name,
                MovementDate = x.CreatedDate,
                Id = x.Id,
                Observations = x.Observations,
                MovementType = x.MovementType.Name,
                AmountIn = 0,
            }));

            summaryList.AddRange(serviceMovement.Select(x => new SummaryViewModel()
            {
                AmountOut = x.Amount,
                AgentName = x.Agent.Name,
                MovementDate = x.CreatedDate,
                Id = x.Id,
                Observations = x.Observations,
                MovementType = x.MovementType.Name,
                AmountIn = 0,
            }));

            summaryList.AddRange(incomes.Select(x => new SummaryViewModel()
            {
                AmountOut = 0,
                AgentName = x.Agent.Name,
                MovementDate = x.CreatedDate,
                Id = x.Id,
                Observations = x.Observations,
                MovementType = x.MovementType.Name,
                AmountIn = x.Amount,
            }));

            summaryList = summaryList.OrderBy(x => x.MovementDate).ToList();

            foreach (var move in summaryList)
            {
                prevTotal += move.AmountIn - move.AmountOut;
                move.Total = prevTotal;
                move.MovementDate = move.MovementDate.ToLocalTime();
            }

            return Json(summaryList.ToDataSourceResult(request));
        }

        [AccessControlAttribute]
        public ActionResult GetMovementTypes()
        {
            IQueryable<MovementType> movementsTypes = _db.MovementType;

            return Json(movementsTypes.Select(e => e.Name).Distinct(), JsonRequestBehavior.AllowGet);
        }


        private static void MappAllViewFields(DataSourceRequest request)
        {
            var rw = request.Filters.ToList();
            foreach (var f in rw)
            {
                MappViewFields(f, "MovementType", "MovementType.Name");
            }
        }

        [AccessControlAttribute]
        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
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
                    if (fd.Operator.ToString().ToLower().Contains("lessthan"))
                    {
                        _upperLimitDate = (DateTime)fd.Value;
                    }
                    if (fd.Operator.ToString().ToLower().Contains("greaterthan"))
                    {
                        _lowerLimitDate = (DateTime)fd.Value;
                    }
                }
            }
        }

    }
}