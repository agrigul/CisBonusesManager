using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Web.Filters;
using Web.Models;
using Web.Infrastructure.Repository;
using Web.Models.Bonuses;

namespace Web.Controllers
{
    /// <summary>
    /// Class BonusesController
    /// </summary>
    [Authorize]
    [InitializeSimpleMembership]
    public class BonusesController : Controller
    {
        /// <summary>
        /// The server error message prefix
        /// </summary>
        private const string ServerErrorMsg = "Server error:";

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

            

            PagedResponse<BonusAggregate> bonuses;
            try
            {
                if (Request != null)
                {
                    sortingField = Request.Params["sort[0][field]"];
                    sortDirection = Request.Params["sort[0][dir]"];
                    filterField = Request.Params["filter[filters][0][field]"];
                    filterValue = Request.Params["filter[filters][0][value]"];
                }
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
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(string.Format("{0} database read bonuses failed: {1}", ServerErrorMsg, ex.Message),
                            JsonRequestBehavior.AllowGet);
            }

            return Json(bonuses, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the list of employees by last name.
        /// </summary>
        /// <returns>JsonResult.</returns>
        public JsonResult GetJsonEmployeesByLastName()
        {
            IList<Employee> employees = new List<Employee>();
            string lastName = Request.Params["filter[filters][0][value]"];
            
            try
            {
                if (String.IsNullOrEmpty(lastName) == false)
                {
                    using (var dbContext = new DatabaseContext())
                    {
                        var employeesRepository = new EmployeesRepository(dbContext);
                        employees = employeesRepository.FindByLastName(lastName);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(string.Format("{0} Database read employees failed: {1}", ServerErrorMsg, ex.Message), 
                            JsonRequestBehavior.AllowGet);
            }
            
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Creates the specified bonus.
        /// </summary>
        /// <param name="bonusDto">The bonus DTO object.</param>
        /// <returns>Http status code result.</returns>
        [HttpPost]
        public JsonResult Create(BonusDto bonusDto)
        {
            BonusAggregate bonus;
            try
            {
                if (bonusDto.Amount <= 0)
                    throw new ArgumentOutOfRangeException("amount should be more than 0");

                if(bonusDto.EmployeeId  <= 0)
                    throw new ArgumentNullException("you should specify an existing employee");

                using (var dbContext = new DatabaseContext())
                {
                    BonusesRepository = new BonusesRepository(dbContext);
                    bonus = new BonusFactory().Create(bonusDto);
                    BonusesRepository.Save(bonus);
                }

                Response.StatusCode = (int)HttpStatusCode.OK;
                
            }
            catch (Exception ex)
            {
                //throw new Exception(string.Format("{0} the create bonus failed: {1}", ServerErrorMsg, ex.Message));
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return  Json(string.Format("{0} create bonus failed: {1}", ServerErrorMsg, ex.Message));
            }

            return Json(bonus);

        }



        /// <summary>
        /// Edits the specified BonusAggregate.
        /// </summary>
        /// <param name="bonusDto">The bonus DTO object.</param>
        /// <returns>Http status code result..</returns>
        [HttpPut]
        public JsonResult Edit(BonusDto bonusDto)
        {
            Employee employee = null;
            try
            {
                if (bonusDto == null)
                    throw new ArgumentNullException("bonusDto can\'t be null in controller Edit");

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
                Response.StatusCode = (int)HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(string.Format("{0} database updated bonus failed: {1}", ServerErrorMsg, ex.Message));
            }

            return Json(employee);

        }
    }
}