using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Web.Models;
using Web.Models.Repositories;

namespace Web.Infrastructure.Repository
{
    /// <summary>
    /// BonusesRepository of bonuses
    /// </summary>
    public class BonusesRepository : IRepository<Bonus>
    {
        /// <summary>
        /// The context of database
        /// </summary>
        private DatabaseContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BonusesRepository"/> class by default.
        /// </summary>
        public BonusesRepository()
        {
            dbContext = new DatabaseContext();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing,
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (dbContext == null) return;

            dbContext.Dispose();
            dbContext = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BonusesRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public BonusesRepository(DatabaseContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("BonusesRepository",
                                                "DatabaseContext should be initialized first");

            this.dbContext = dbContext;
        }

        /// <summary>
        /// Finds all bonuses.
        /// </summary>
        /// <returns>IEnumerable{Bonus}.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<Bonus> FindAll()
        {
            return dbContext.Bonuses
                    .Include(b => b.Employee)
                    .ToList();
        }

        /// <summary>
        /// Gets bonus by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Bonus.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Bonus GetById(int id)
        {
            return dbContext.Bonuses
                    .Include(b => b.Employee)
                    .Where(x => x.Id == id).FirstOrDefault();
        }
    }
}