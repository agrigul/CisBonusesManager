using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Web.Models.Bonuses;

namespace Web.Infrastructure.Repository
{
    /// <summary>
    /// BonusesRepository of bonuses
    /// </summary>
    public class BonusesRepository : IRepository<BonusAggregate>
    {
        /// <summary>
        /// The context of database
        /// </summary>
        private DatabaseContext dbContext;

        /// <summary>
        /// The db set
        /// </summary>
        /// <value>The db set.</value>
        private DbSet<BonusAggregate> DbSet
        {
            get { return dbContext.Bonuses; }
        }

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
        /// <returns>IEnumerable{BonusAggregate}.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<BonusAggregate> FindAll()
        {
            return DbSet.Include(b => b.Employee)
                        .ToList();
        }

        /// <summary>
        /// Gets bonus by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>BonusAggregate.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public BonusAggregate GetById(int id)
        {
            return DbSet.Include(b => b.Employee)
                        .Where(x => x.BonusId == id)
                        .FirstOrDefault();
        }

        /// <summary>
        /// Saves the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="System.ArgumentNullException">Save;BonusAggregate item shouldn't be null</exception>
        public void Save(BonusAggregate item)
        {
           Save(new List<BonusAggregate>{item});
        }

        /// <summary>
        /// Saves the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <exception cref="System.ArgumentNullException">Save;List of Bonuses shouldn't be null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Save;List of Bonuses can't be empty</exception>
        public void Save(IEnumerable<BonusAggregate> items)
        {
            if (items == null)
                throw new ArgumentNullException("Save", "List of Bonuses shouldn't be null");

            if (!items.Any())
                throw new ArgumentOutOfRangeException("Save", "List of Bonuses can't be empty");

            try
            {
                var employeesRepository = new EmployeesRepository(dbContext);
                
                //context doesn't want to save correctly without previous request
                List<int> employees = (from b in items 
                                       select b.Employee.EmployeeId).ToList();
                IList<Employee> attachedEmployees = employeesRepository.GetByIdList(employees);
                
                foreach (BonusAggregate bonus in items)
                {
                    bonus.Employee = (from e in attachedEmployees
                                      where bonus.Employee.EmployeeId == e.EmployeeId
                                      select e).First();

                    if ((dbContext.Entry(bonus).State == EntityState.Detached ) ||
                         bonus.EmployeeId == 0)
                        DbSet.Add(bonus);
                }

//                   var currentEmployee= dbContext.Employees.Find(bonus.Employee.EmployeeId); // doesn't make a request to db
//                   dbContext.Entry(currentEmployee).State = EntityState.Unchanged;
//                   dbContext.Entry(currentEmployee).CurrentValues.SetValues(bonus.Employee);
//                    dbContext.Bonuses.Attach(bonus);
//                    dbContext.Entry(bonus).State = EntityState.Modified;

                    // DbSet.Add(bonus);
                
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine("---EXCEPTION: " + e.InnerException.Message);

                if (e.InnerException.InnerException != null)
                    System.Diagnostics.Debug.WriteLine("---INNER: " + e.InnerException.InnerException.Message);
#endif
                throw;
            }
        }
       
    }
}