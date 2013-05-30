using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Web.Controllers.Attributes;
using Web.Filters;
using Web.Infrastructure.Repository;
using Web.Models.Bonuses;
using Web.Models.Employees;
using Web.Models.Factories;
using Web.Models.ValueObjects;

namespace Web.Controllers
{
    /// <summary>
    /// Class BonusesController recevies requests from grid and displays bobuses
    /// </summary>
    [Authorize]
    [InitializeSimpleMembership]
    public class BonusesController : Controller
    {
        /// <summary>
        /// The repository of bonuses
        /// </summary>
        private IRepository<BonusAggregate> BonusesRepository { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="BonusesController"/> class by default.
        /// </summary>
        public BonusesController()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BonusesController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public BonusesController(IRepository<BonusAggregate> repository)
        {
            BonusesRepository = repository;
        }

        /// <summary>
        /// Returns a page with bonuses table
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets the paged bonuses list for table.
        /// </summary>
        /// <param name="take">Records to take.</param>
        /// <param name="skip">Records to skip.</param>
        /// <returns>JsonResult of bonuses</returns>
        [AjaxErrorFilter]
        public JsonResult GetPagedJsonBonuses(int take, int skip)
        {
            PagedResponse<BonusAggregate> bonuses;

            var filteredRequest = new FilteredRequest(Request.Params);

            using (var dbContext = new DatabaseContext())
            {
                BonusesRepository = new BonusesRepository(dbContext);
                bonuses = BonusesRepository.FindAll(skip,
                                                    take,
                                                    filteredRequest.SortingField,
                                                    filteredRequest.Direction,
                                                    filteredRequest.FilterField,
                                                    filteredRequest.FilterPattern);
            }

            return Json(bonuses, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// Gets the list of employees by last name.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [AjaxErrorFilter]
        public JsonResult GetJsonEmployeesByLastName()
        {
            IList<Employee> employees = new List<Employee>();
            string lastName = FilterStringFactory.FormFilterValue(Request.Params);


            if (String.IsNullOrEmpty(lastName) == false)
            {
                using (var dbContext = new DatabaseContext())
                {
                    var employeesRepository = new EmployeesRepository(dbContext);
                    employees = employeesRepository.FindByLastName(lastName);
                }
            }


            if (employees.Count == 0)                    // this's because some magic happens with autocomplete combobox 
                employees.Add(new Employee("", "", "")); // and it start to cycle ajax requests if the list is empty or null

            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Creates the specified bonus according to BonusDTO.
        /// </summary>
        /// <param name="bonusDto">The bonus DTO object.</param>
        /// <returns>Http status code result.</returns>
        [AjaxErrorFilter]
        [HttpPost]
        public JsonResult Create(BonusDto bonusDto)
        {
            BonusAggregate bonus;
            
            if (bonusDto.Amount <= 0)
                throw new ArgumentOutOfRangeException("Amount should be more than 0");

            if (bonusDto.EmployeeId <= 0)
                throw new ArgumentNullException("You should specify an existing employee");

            SetDlcAndUlc(bonusDto);

            using (var dbContext = new DatabaseContext())
            {
                BonusesRepository = new BonusesRepository(dbContext);
                var employeeRepository = new EmployeesRepository(dbContext);
                bonus = new BonusFactory(employeeRepository).Create(bonusDto);

                BonusesRepository.Save(bonus);
            }


            // null or void throws exception in Jquery 1.9 as invalid result and 
            // return of bonus as a result duplicates records.
            HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
            return Json(bonus);
        }
        
        /// <summary>
        /// Edits the specified BonusAggregate.
        /// </summary>
        /// <param name="bonusDto">The bonus DTO object.</param>
        /// <returns>Http status code result..</returns>
        [AjaxErrorFilter]
        [HttpPost]
        public JsonResult Edit(BonusDto bonusDto)
        {
            if (bonusDto == null)
                throw new ArgumentNullException("bonusDto can not be null in controller Edit");

            SetDlcAndUlc(bonusDto);

            Employee employee = null;
            BonusAggregate bonus;
            using (var dbContext = new DatabaseContext())
            {
                if (bonusDto.EmployeeId != 0)
                {
                    var employeeRepository = new EmployeesRepository(dbContext);
                    employee = employeeRepository.GetById(bonusDto.EmployeeId);
                }

                BonusesRepository = new BonusesRepository(dbContext);
                bonus = BonusesRepository.GetById(bonusDto.BonusId);
                bonus.Comment = bonusDto.Comment;
                bonus.Amount = bonusDto.Amount;
                bonus.Date = bonusDto.Date;
                bonus.IsActive = bonusDto.IsActive;

                if (employee != null &&
                    employee.EmployeeId != bonus.EmployeeId)
                {
                    bonus.Employee = employee;
                }

                BonusesRepository.Save(bonus);
            }

            return Json(bonus);
        }


        /// <summary>
        /// Sets the default values for DLC and Ulc.
        /// </summary>
        /// <param name="bonusDto">The bonus dto.</param>
        private static void SetDlcAndUlc(BonusDto bonusDto)
        {
            if (bonusDto == null)
                throw new ArgumentNullException("BonusDto is null in SetDlcAndUlc method");

            if (bonusDto.Ulc == null || string.IsNullOrEmpty(bonusDto.Ulc.Trim()))
                bonusDto.Ulc = SessionRepository.GetUserCredentials().UserName;

            if (bonusDto.Dlc == DateTime.MinValue)
                bonusDto.Dlc = DateTime.Now;
        }
    }
}