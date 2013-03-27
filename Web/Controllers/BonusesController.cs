using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models;
using Web.Infrastructure.Repository;
using Web.Models.Repositories;

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
        private IRepository<Bonus> Repository { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BonusesController"/> class by default.
        /// </summary>
        public BonusesController()
        {
            Repository = new BonusesRepository();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BonusesController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public BonusesController(IRepository<Bonus> repository)
        {
            Repository = repository;
        }

        // GET: /Bonuses/

        /// <summary>
        /// Returns all bonuses
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            IList<Bonus> bonuses;

            using (Repository)
            {
                bonuses = Repository.FindAll();
            }

            return View(bonuses);
        }

        // GET: /Bonuses/Details/5
        /// <summary>
        /// Returns the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Details(int id = 0)
        {
            //Bonus bonus = db.Bonuses.Find(id);
            Bonus bonus;
            using (Repository)
            {
                bonus = Repository.GetById(id);
            }

            if (bonus == null)
            {
                return HttpNotFound();
            }
            return View(bonus);
        }

        //
        // GET: /Bonuses/Create

        /// <summary>
        /// Creates new bonus.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Create()
        {
         //   ViewBag.Id = new SelectList(db.Employees, "Id", "UserName");
            return View();
        }

        //
        // POST: /Bonuses/Create

        /// <summary>
        /// Creates the specified bonus.
        /// </summary>
        /// <param name="bonus">The bonus.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Create(Bonus bonus)
        {
//            if (ModelState.IsValid)
//            {
//                db.Bonuses.Add(bonus);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//
//            ViewBag.Id = new SelectList(db.Employees, "Id", "UserName", bonus.Id);
            return View(bonus);
        }

        //
        // GET: /Bonuses/Edit/5

        public ActionResult Edit(int id = 0)
        {
            //Bonus bonus = db.Bonuses.Find(id);
            Bonus bonus;
            using(Repository)
            {
                bonus = Repository.GetById(id);
            }

            if (bonus == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Id = new SelectList(db.Employees, "Id", "UserName", bonus.Id);
            return View(bonus);
        }

        //
        // POST: /Bonuses/Edit/5

        /// <summary>
        /// Edits the specified bonus.
        /// </summary>
        /// <param name="bonus">The bonus.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Edit(Bonus bonus)
        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(bonus).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.Id = new SelectList(db.Employees, "Id", "UserName", bonus.Id);
            return View(bonus);
        }

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}