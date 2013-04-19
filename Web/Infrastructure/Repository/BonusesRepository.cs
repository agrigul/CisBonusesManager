using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Web.Models.Bonuses;
using Web.Models.Employee;
using Web.Models.Factories;
using Web.Models.ValueObjects;

namespace Web.Infrastructure.Repository
{
    /// <summary>
    /// BonusesRepository of bonuses
    /// </summary>
    public class BonusesRepository : IRepository<BonusAggregate>
    {
        //Thu Apr 04 2013 00:00:00 GMT+0300 (FLE Daylight Time)"
        /// <summary>
        /// The context of database
        /// </summary>
        private readonly DatabaseContext dbContext;

        /// <summary>
        /// The bonuses filter
        /// </summary>
        private readonly BonusesFilterQueryBuilder bonusesFilter;

        /// <summary>
        /// The bonuses sorting
        /// </summary>
        private readonly BonusesSortingQueryBuilder bonusesSorting;

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
            bonusesSorting = new BonusesSortingQueryBuilder();
            bonusesFilter = new BonusesFilterQueryBuilder();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BonusesRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public BonusesRepository(DatabaseContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("BonusesRepository",
                                                "databaseContext should be initialized first");
            this.dbContext = dbContext;

            bonusesSorting = new BonusesSortingQueryBuilder();
            bonusesFilter = new BonusesFilterQueryBuilder();
        }

        /// <summary>
        /// Finds all bonuses.
        /// </summary>
        /// <returns>IEnumerable{BonusAggregate}.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<BonusAggregate> FindAll()
        {
            return DbSet.Include(b => b.Employee)
                        .OrderByDescending(x => x.BonusId)
                        .ToList();
        }

        /// <summary>
        /// Fins all with paging.
        /// </summary>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>PagedResponse{BonusAggregate}.</returns>
        public PagedResponse<BonusAggregate> FindAllWithPaging(int skip, int take)
        {
            List<BonusAggregate> result = DbSet.Include(b => b.Employee)
                                               .OrderByDescending(x => x.BonusId)
                                               .Skip(skip)
                                               .Take(take)
                                               .ToList();

            int numberOfItemsInDb = DbSet.Count();
            return new PagedResponse<BonusAggregate>(result, numberOfItemsInDb);

        }


        /// <summary>
        /// Finds all with paging with sorting.
        /// </summary>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="filterField">The filter field.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <returns>PagedResponse{BonusAggregate}.</returns>
        public PagedResponse<BonusAggregate> FindAll(int skip,
                                                        int take,
                                                        string sortField,
                                                        SortingDirection sortDirection,
                                                        string filterField,
                                                        string filterValue)
        {
            IQueryable<BonusAggregate> query = DbSet.Include(b => b.Employee);
            query = bonusesFilter.BuildFilter(filterField, filterValue, query);

            int numberOfItemsInDb = query.Count();
            query = bonusesSorting.BuildQuery(sortField, sortDirection, query);

            List<BonusAggregate> result = (query.Skip(skip)
                                            .Take(take)).ToList();

            return new PagedResponse<BonusAggregate>(result, numberOfItemsInDb);
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
        /// <exception cref="System.ArgumentNullException">Save;BonusAggregate item shouldn not be null</exception>
        public void Save(BonusAggregate item)
        {
            Save(new List<BonusAggregate> { item });
        }

        /// <summary>
        /// Saves the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <exception cref="System.ArgumentNullException">Save;List of Bonuses shouldn not be null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Save;List of Bonuses can not be empty</exception>
        public void Save(IEnumerable<BonusAggregate> items)
        {
            if (items == null)
                throw new ArgumentNullException("Save", "list of Bonuses should not be null");

            if (!items.Any())
                throw new ArgumentOutOfRangeException("Save", "list of Bonuses can not be empty");


            var employeesRepository = new EmployeesRepository(dbContext);

            //context doesn not want to save correctly without previous request
            List<int> employees = (from b in items
                                   select b.EmployeeId).ToList();
            IList<Employee> attachedEmployees = employeesRepository.GetByIdList(employees);

            foreach (BonusAggregate bonus in items)
            {
                bonus.Employee = (from e in attachedEmployees
                                  where bonus.EmployeeId == e.EmployeeId
                                  select e).First();

                if ((dbContext.Entry(bonus).State == EntityState.Detached) &&
                     bonus.BonusId == 0)
                    DbSet.Add(bonus); // Create

                if ((dbContext.Entry(bonus).State == EntityState.Detached) &&
                     bonus.BonusId != 0)
                {
                    (dbContext.Entry(bonus)).State = EntityState.Modified;
                    DbSet.Attach(bonus); // Update
                }
            }

            dbContext.SaveChanges();

        }

    }
}