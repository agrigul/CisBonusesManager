using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models;
using Web.Models.Repositories;

namespace Web.Infrastructure.Repository
{
    /// <summary>
    /// BonusesRepository of bonuses
    /// </summary>
    public class EmployeesRepository : IRepository<Employee>
    {
        /// <summary>
        /// The context of database
        /// </summary>
        private DatabaseContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BonusesRepository"/> class by default.
        /// </summary>
        public EmployeesRepository()
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
        public EmployeesRepository(DatabaseContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("EmployeesRepository",
                                                "DatabaseContext should be initialized first");

            this.dbContext = dbContext;
        }

        /// <summary>
        /// Finds all Employees.
        /// </summary>
        /// <returns>IEnumerable{Employee}.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<Employee> FindAll()
        {
            return dbContext.Employees.ToList();
        }

        /// <summary>
        /// Gets Employee by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Employee.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Employee GetById(int id)
        {
            return dbContext.Employees
                    .Where(x => x.Id == id)
                    .FirstOrDefault();
        }


    }
}