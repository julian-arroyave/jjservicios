﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using JJServicios.DB.Contracts;
﻿using JJServicios.DB.Contracts.Repositories;
﻿using JJServicios.Web.Models;
﻿using Kendo.Mvc;

namespace JJServicios.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly JJServiciosEntities _db = new JJServiciosEntities();

        private readonly IDeleteAdoRepository _dbAdoRepository;

        public EmployeeController(IDeleteAdoRepository dbAdoRepository)
        {
            _dbAdoRepository = dbAdoRepository;
        }

        [AccessControlAttribute]
        public ActionResult Index()
        {
            IQueryable<MovementType> movementTypes = _db.MovementType;
            ViewData["MovementTypes"] = new SelectList(movementTypes, "Id", "Name");
            IQueryable<Employee> employees = _db.Employee;
            var basicEmployees = employees.Select(x => new { x.Id,  x.Name });
            ViewData["Employees"] = new SelectList(basicEmployees, "Id", "Name");
            IQueryable<FinancialAccount> financialAccounts = _db.FinancialAccount;
            ViewData["FinancialAccounts"] = new SelectList(financialAccounts, "Id", "Name");
            IQueryable<EmployeePosition> employeePositions = _db.EmployeePosition;
            ViewData["EmployeePosition"] = new SelectList(employeePositions, "Id", "Name");
            return View();
        }

        [AccessControlAttribute]
        public ActionResult Employee_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Employee> employees = _db.Employee;
            employees = employees.Where(x => x.Active);
            MappAllViewFields(request);
            DataSourceResult result = employees.ToDataSourceResult(request, employee => new EmployeeViewModel
            {
                Id = employee.Id,
                Identification = employee.Identification,
                Name = employee.Name,
                FinancialNumber = employee.FinancialNumber,
                Mobile = employee.Mobile,
                OtherPhone = employee.OtherPhone,
                WhatsApp = employee.WhatsApp,
                Skype = employee.Skype,
                CorporateEmail = employee.CorporateEmail,
                OtherEmail = employee.OtherEmail,
                ResidenceCity = employee.ResidenceCity,
                Address = employee.Address,
                Active = employee.Active,
                Birthdate = employee.Birthdate,
                CreatedDate = employee.CreatedDate.ToLocalTime(),
                UpdateDate = employee.UpdateDate.ToLocalTime(),
                EmployeePosition = employee.EmployeePosition.Name,
                FinancialAccount = employee.FinancialAccount.Name,
                
            });

            return Json(result);
        }

        [AccessControlAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Employee_Create([DataSourceRequest]DataSourceRequest request, EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                var entity = new Employee
                {
                    AgentId = AuthenticationHelper.AuthenticationHelper.GetAgentId(),
                Identification = employee.Identification,
                    Name = employee.Name,
                    FinancialNumber = employee.FinancialNumber,
                    Mobile = employee.Mobile,
                    OtherPhone = employee.OtherPhone,
                    WhatsApp = employee.WhatsApp,
                    Skype = employee.Skype,
                    CorporateEmail = employee.CorporateEmail,
                    OtherEmail = employee.OtherEmail,
                    ResidenceCity = employee.ResidenceCity,
                    Address = employee.Address,
                    Active = true,
                    EmployeePositionId = employee.EmployeePositionId,
                    FinancialAccountId = employee.FinancialAccountId,
                    Birthdate = employee.Birthdate,
                    CreatedDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                };

                _db.Employee.Add(entity);
                _db.SaveChanges();
                employee.Id = entity.Id;
            }

            return Json(new[] { employee }.ToDataSourceResult(request, ModelState));
        }

        [AccessControlAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Employee_Update([DataSourceRequest]DataSourceRequest request, EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                var entity = new Employee
                {
                    AgentId = AuthenticationHelper.AuthenticationHelper.GetAgentId(),
                    Id = employee.Id,
                    Identification = employee.Identification,
                    Name = employee.Name,
                    FinancialNumber = employee.FinancialNumber,
                    Mobile = employee.Mobile,
                    OtherPhone = employee.OtherPhone,
                    WhatsApp = employee.WhatsApp,
                    Skype = employee.Skype,
                    CorporateEmail = employee.CorporateEmail,
                    OtherEmail = employee.OtherEmail,
                    ResidenceCity = employee.ResidenceCity,
                    Address = employee.Address,
                    Active = employee.Active,
                    EmployeePositionId = employee.EmployeePositionId,
                    FinancialAccountId = employee.FinancialAccountId,
                    Birthdate = employee.Birthdate,
                    CreatedDate = employee.CreatedDate.ToUniversalTime(),
                    UpdateDate =  DateTime.UtcNow

                };

                _db.Employee.Attach(entity);
                _db.Entry(entity).State = EntityState.Modified;
                _db.SaveChanges();
            }

            return Json(new[] { employee }.ToDataSourceResult(request, ModelState));
        }

        [AccessControlAttribute]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Employee_Destroy([DataSourceRequest]DataSourceRequest request, Employee serviceMovement)
        {
            _dbAdoRepository.DeleteItemById(serviceMovement.Id, "Employee");

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

        private static void MappAllViewFields(DataSourceRequest request)
        {
            var rw = request.Filters.ToList();
            foreach (var f in rw)
            {
                MappViewFields(f, "EmployeePosition", "EmployeePosition.Name");
                MappViewFields(f, "FinancialAccount", "FinancialAccount.Name");
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

    }
}
