﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Globalization;
using System.Linq;
using Web.Models;
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
            //dbContext = new DatabaseContext();
        }

//        /// <summary>
//        /// Performs application-defined tasks associated with freeing,
//        /// releasing, or resetting unmanaged resources.
//        /// </summary>
//        public void Dispose()
//        {
//            if (dbContext == null) return;
//
//            dbContext.Dispose();
//            dbContext = null;
//        }

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
        public PagedResponse<BonusAggregate> FindAll(int skip, int take,
            string sortField,
            SortingDirection sortDirection,
            string filterField,
            string filterValue)
        {
            IQueryable<BonusAggregate> query = DbSet.Include(b => b.Employee);
            query = SetFiltering(filterField, filterValue, query);

            int numberOfItemsInDb = query.Count();

            query = SetSorting(sortField, sortDirection, query);

            List<BonusAggregate> result = (query.Skip(skip)
                          .Take(take)).ToList();

            return new PagedResponse<BonusAggregate>(result, numberOfItemsInDb);
        }

        /// <summary>
        /// Sets the filtering.
        /// </summary>
        /// <param name="filterField">The filter field.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="query">The query.</param>
        /// <returns>IQueryable{BonusAggregate}.</returns>
        private IQueryable<BonusAggregate> SetFiltering(string filterField, string filterValue, IQueryable<BonusAggregate> query)
        {
            //Thu Apr 04 2013 00:00:00 GMT+0300 (FLE Daylight Time)"
            const string dateTimeFormat = "ddd MMM dd yyyy hh:mm:ss"; // string format from controll

            switch (filterField)
            {
                case "EmployeeLastName":
                    query = query.Where(x => x.Employee.LastName.Contains(filterValue));
                    break;

                case "Date":
                    filterValue = filterValue.Remove(filterValue.IndexOf('G')).Trim();
                    DateTime date = DateTime.ParseExact(filterValue, dateTimeFormat, CultureInfo.InvariantCulture);
                    query = query.Where(x => (x.Date.Year == date.Year &&
                                              x.Date.Month == date.Month &&
                                              x.Date.Day == date.Day));
                    break;

                case "Amount":
                    decimal amountValue = decimal.Parse(filterValue, CultureInfo.CreateSpecificCulture("en-GB"));
                    query = query.Where(x => x.Amount == amountValue);
                    break;

                case "Comment":
                    query = query.Where(x => x.Comment.Contains(filterValue));
                    break;

                case "IsActive":
                    bool isActiveValue = bool.Parse(filterValue);
                    query = query.Where(x => x.IsActive == isActiveValue);
                    break;

                case "Ulc":
                    query = query.Where(x => x.Ulc.Contains(filterValue));
                    break;

                case "Dlc":
                    filterValue = filterValue.Remove(filterValue.IndexOf('G')).Trim();
                    DateTime dlc = DateTime.ParseExact(filterValue, dateTimeFormat, CultureInfo.InvariantCulture);
                    query = query.Where(x => (x.Date.Year == dlc.Year &&
                                              x.Date.Month == dlc.Month &&
                                              x.Date.Day == dlc.Day));
                    break;
            }

            return query;
        }

        /// <summary>
        /// Sets the sorting settings.
        /// </summary>
        /// <param name="sortField">The sort field.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="query">The query.</param>
        /// <returns>IQueryable{BonusAggregate}.</returns>
        private IQueryable<BonusAggregate> SetSorting(string sortField, SortingDirection sortDirection, IQueryable<BonusAggregate> query)
        {

            switch (sortField)
            {
                case "EmployeeLastName":
                    query = sortDirection == SortingDirection.Asc
                                ? query.OrderBy(x => x.Employee.LastName)
                                : query.OrderByDescending(x => x.Employee.LastName);
                    break;
                case "Date":
                    query = sortDirection == SortingDirection.Asc
                                ? query.OrderBy(x => x.Date)
                                : query.OrderByDescending(x => x.Date);
                    break;
                case "Amount":
                    query = sortDirection == SortingDirection.Asc
                                ? query.OrderBy(x => x.Amount)
                                : query.OrderByDescending(x => x.Amount);
                    break;
                case "Comment":
                    query = sortDirection == SortingDirection.Asc
                                ? query.OrderBy(x => x.Comment)
                                : query.OrderByDescending(x => x.Comment);
                    break;
                case "IsActive":
                    query = sortDirection == SortingDirection.Asc
                                ? query.OrderBy(x => x.IsActive)
                                : query.OrderByDescending(x => x.IsActive);
                    break;
                case "Ulc":
                    query = sortDirection == SortingDirection.Asc
                                ? query.OrderBy(x => x.Ulc)
                                : query.OrderByDescending(x => x.Ulc);
                    break;
                case "Dlc":
                    query = sortDirection == SortingDirection.Asc
                                ? query.OrderBy(x => x.Dlc)
                                : query.OrderByDescending(x => x.Dlc);
                    break;
                default:
                    query = query.OrderByDescending(x => x.BonusId);
                    break;
            }
            return query;
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
                throw new ArgumentNullException("Save", "List of Bonuses should not be null");

            if (!items.Any())
                throw new ArgumentOutOfRangeException("Save", "List of Bonuses can not be empty");


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