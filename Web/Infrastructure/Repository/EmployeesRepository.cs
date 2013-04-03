using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Web.Models;
using Web.Models.Bonuses;

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
        /// Gets the db set.
        /// </summary>
        /// <value>The db set.</value>
        private DbSet<Employee> DbSet { get { return dbContext.Employees; } }

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
            return DbSet.ToList();
        }

        /// <summary>
        /// Finds all with paging.
        /// </summary>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>PagedResponse{Employee}.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public PagedResponse<Employee> FindAllWithPaging(int skip, int take)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets Employee by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Employee.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Employee GetById(int id)
        {
            return DbSet.Where(x => x.EmployeeId == id)
                        .FirstOrDefault();
        }

        /// <summary>
        /// Gets the list of employees by list of employeeId.
        /// </summary>
        /// <param name="ids">The employees' ids.</param>
        /// <returns>IList{Employee}.</returns>
        public IList<Employee> GetByIdList(IEnumerable<int> ids)
        {
           return (from e in DbSet
                    where ids.Contains(e.EmployeeId)
                    select e).ToList();
        }

        /// <summary>
        /// Saves the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <exception cref="System.ArgumentNullException">Save;List of Employee shouldn't be null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Save;List of Employee can't be empty</exception>
        public void Save(IEnumerable<Employee> items)
        {
            if (items == null)
                throw new ArgumentNullException("Save", "List of Employee shouldn't be null");

            if (!items.Any())
                throw new ArgumentOutOfRangeException("Save", "List of Employee can't be empty");

            foreach (Employee employee in items)
            {
                DbSet.Add(employee);
            }

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Saves the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="System.ArgumentNullException">Save;Employee item shouldn't be null</exception>
        public void Save(Employee item)
        {
            throw new NotSupportedException("Employee entities can't be saved");
        }
    }
}