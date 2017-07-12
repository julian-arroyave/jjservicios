﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JJServicios.DB.Contracts;
using JJServicios.Web.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;

namespace JJServicios.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JJServiciosEntities _db = new JJServiciosEntities();

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [AccessControlAttribute]
        public ActionResult Index()
        {
            var user = User.Identity.GetUserName();
            return View();
        }

        [AccessControlAttribute]
        public ActionResult Agent_Read([DataSourceRequest]DataSourceRequest request)
        {
            var limitDate = DateTime.Today.AddDays(-60);
            IQueryable<Expense> expenses = _db.Expense.Where(x => x.CreatedDate >= limitDate);
            IQueryable<Income> incomes = _db.Income.Where(x => x.CreatedDate >= limitDate); ;
            IQueryable<ServiceMovement> serviceMovement = _db.ServiceMovement.Where(x => x.CreatedDate >= limitDate);


            IQueryable<Expense> prevExpenses = _db.Expense.Where(x => x.CreatedDate < limitDate);
            IQueryable<Income> prevIncomes = _db.Income.Where(x => x.CreatedDate < limitDate); ;
            IQueryable<ServiceMovement> prevServiceMovement = _db.ServiceMovement.Where(x => x.CreatedDate < limitDate);

            var amount = prevExpenses.ToList();

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

    }
}