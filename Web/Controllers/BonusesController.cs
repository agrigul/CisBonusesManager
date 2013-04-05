using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Web.Models;
using Web.Infrastructure.Repository;
using Web.Models.Bonuses;

namespace Web.Controllers
{
    /// <summary>
    /// Class BonusesController
    /// </summary>
    public class BonusesController : Controller
    {
        /// <summary>
        /// The repository of bonuses
        /// </summary>
        private IRepository<BonusAggregate> BonusesRepository { get; set; }

        private IRepository<EmployeesRepository> EmployeesRepository { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BonusesController"/> class by default.
        /// </summary>
        public BonusesController()
        {
            BonusesRepository = new BonusesRepository(); // dbContext from config
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BonusesController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public BonusesController(IRepository<BonusAggregate> repository)
        {
            BonusesRepository = repository;
        }

        // GET: /Bonuses/

        /// <summary>
        /// Returns all bonuses
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
        public JsonResult GetPagedJsonBonuses(int take, int skip)
        {
            string sortingField = String.Empty;
            string sortDirection = String.Empty;
            string filterField = String.Empty;
            string filterValue = String.Empty;

            if (Request != null)
            {
                sortingField = Request.Params["sort[0][field]"];
                sortDirection = Request.Params["sort[0][dir]"];
                filterField = Request.Params["filter[filters][0][field]"];
                filterValue = Request.Params["filter[filters][0][value]"];
            }

            PagedResponse<BonusAggregate> bonuses;
            SortingDirection direction;

            if (String.IsNullOrEmpty(sortDirection))
            {
                direction = SortingDirection.Desc;
            }
            else
            {
                direction = sortDirection == "asc" ? SortingDirection.Asc : SortingDirection.Desc;
            }

            using (var dbContext = new DatabaseContext())
            {
                BonusesRepository = new BonusesRepository(dbContext);
                bonuses = BonusesRepository.FindAll(skip, take, sortingField, direction, filterField, filterValue);
            }


            return Json(bonuses, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the list of employees by last name.
        /// </summary>
        /// <returns>JsonResult.</returns>
        public JsonResult GetJsonEmployeesByLastName()
        {
            string lastName = Request.Params["filter[filters][0][value]"];

            IList<Employee> employees = new List<Employee>();

            if (String.IsNullOrEmpty(lastName) == false)
            {
                using (var dbContext = new DatabaseContext())
                {
                    var employeesRepository = new EmployeesRepository(dbContext);
                    employees = employeesRepository.FindByLastName(lastName);
                }
            }

            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        //        //
        //        // GET: /Bonuses/Create
        //
        //        /// <summary>
        //        /// Creates new bonus.
        //        /// </summary>
        //        /// <returns>ActionResult.</returns>
        //        public ActionResult Create()
        //        {
        //            //   ViewBag.Id = new SelectList(db.Employees, "Id", "UserName");
        //            return View();
        //        }


        /// <summary>
        /// Creates the specified bonus.
        /// </summary>
        /// <param name="bonusDto">The bonus DTO object.</param>
        /// <returns>Http status code result.</returns>
        [HttpPost]
        public HttpStatusCodeResult Create(BonusDto bonusDto)
        {

            using (var dbContext = new DatabaseContext())
            {
                BonusesRepository = new BonusesRepository(dbContext);
                BonusAggregate bonus = new BonusFactory().Create(bonusDto);
                BonusesRepository.Save(bonus);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }



        /// <summary>
        /// Edits the specified BonusAggregate.
        /// </summary>
        /// <param name="bonusDto">The bonus DTO object.</param>
        /// <returns>Http status code result..</returns>
        [HttpPut]
        public HttpStatusCodeResult Edit(BonusDto bonusDto)
        {
            if (bonusDto == null)
                throw new ArgumentNullException("bonusDto can't be null in controller Edit");

            Employee employee = null;
            if (bonusDto.EmployeeId != 0)
                using (var dbContext = new DatabaseContext())
                {
                    var employeeRepository = new EmployeesRepository(dbContext);
                    employee = employeeRepository.GetById(bonusDto.EmployeeId);
                }

            using (var dbContext = new DatabaseContext())
            {
                BonusesRepository = new BonusesRepository(dbContext);
                BonusAggregate bonus = BonusesRepository.GetById(bonusDto.BonusId);
                bonus.Comment = bonusDto.Comment;
                bonus.Amount = bonusDto.Amount;
                bonus.Date = bonusDto.Date;
                bonus.IsActive = bonusDto.IsActive;

                if (employee != null &&
                    employee.EmployeeId != bonus.EmployeeId)
                    bonus.Employee = employee;

                BonusesRepository.Save(bonus);
            }


            return new HttpStatusCodeResult(HttpStatusCode.OK);

        }
    }
}