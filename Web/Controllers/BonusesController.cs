using System;
using System.Collections.Generic;
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
            IList<BonusAggregate> bonuses = BonusesRepository.FindAll();

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
            //BonusAggregate bonus = db.Bonuses.Find(id);

            BonusAggregate bonusAggregate = BonusesRepository.GetById(id);

            if (bonusAggregate == null)
            {
                return HttpNotFound();
            }
            return View(bonusAggregate);
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
        /// <param name="bonusAggregate">The bonus.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Create(BonusAggregate bonusAggregate)
        {
            if (ModelState.IsValid)
            {
                BonusesRepository.Save(bonusAggregate);
            
                //                db.Bonuses.Add(bonus);
                //                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.Id = new SelectList(db.Employees, "Id", "UserName", bonus.Id);
            return View(bonusAggregate);
        }

        //
        // GET: /Bonuses/Edit/5

        public ActionResult Edit(int id = 0)
        {
            //BonusAggregate bonus = db.Bonuses.Find(id);
            BonusAggregate bonusAggregate;
            using (BonusesRepository = new BonusesRepository())
            {
                bonusAggregate = BonusesRepository.GetById(id);
            }

            if (bonusAggregate == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Id = new SelectList(db.Employees, "Id", "UserName", bonus.Id);
            return View(bonusAggregate);
        }

        //
        // POST: /Bonuses/Edit/5

        /// <summary>
        /// Edits the specified BonusAggregate.
        /// </summary>
        /// <param name="bonusAggregate">The bonus.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Edit(BonusAggregate bonusAggregate)
        {
            //            if (ModelState.IsValid)
            //            {
            //                db.Entry(bonus).State = EntityState.Modified;
            //                db.SaveChanges();
            //                return RedirectToAction("Index");
            //            }
            //            ViewBag.Id = new SelectList(db.Employees, "Id", "UserName", bonus.Id);
            return View(bonusAggregate);
        }

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if(BonusesRepository != null)
                BonusesRepository.Dispose();
            BonusesRepository = null;

            base.Dispose(disposing);
        }
    }
}