using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Web.Models.Employees;
using Web.Models.ValueObjects;

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
        private readonly DatabaseContext dbContext;

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
            //dbContext = new DatabaseContext();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BonusesRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public EmployeesRepository(DatabaseContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("EmployeesRepository",
                                                "databaseContext should be initialized first");

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
        /// Finds all.
        /// </summary>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="filterField">The filter field.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <returns>PagedResponse{Employee}.</returns>
        /// <exception cref="System.NotSupportedException">FindAll with filtering is not supported for Employees repository</exception>
        public PagedResponse<Employee> FindAll(int skip, int take, string sortField, SortingDirection sortDirection, string filterField, string filterValue)
        {
            throw new NotSupportedException("FindAll with filtering is not supported for Employees repository");
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
            throw new NotSupportedException("FindAllWithPaging is not supported for Employees repository");
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
        /// <exception cref="System.ArgumentNullException">Save;List of Employee shouldn not be null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Save;List of Employee can not be empty</exception>
        public void Save(IEnumerable<Employee> items)
        {
            if (items == null)
                throw new ArgumentNullException("Save", "list of Employee shouldn not be null");

            if (!items.Any())
                throw new ArgumentOutOfRangeException("Save", "list of Employee can not be empty");

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
        /// <exception cref="System.ArgumentNullException">Save;Employee item shouldn not be null</exception>
        public void Save(Employee item)
        {
            throw new NotSupportedException("employee entities can not be saved");
        }

        /// <summary>
        /// Finds employees the by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>IList{Employee}.</returns>
        public IList<Employee> FindByLastName(string filter)
        {
            return (from e in DbSet
                    where e.LastName.Contains(filter)
                    select e).ToList();
        }
    }
}