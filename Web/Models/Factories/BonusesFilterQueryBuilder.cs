using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Web.Models.Bonuses;

namespace Web.Models.Factories
{
    /// <summary>
    /// Builds a query from filter string
    /// </summary>
    public class BonusesFilterQueryBuilder
    {
        /// <summary>
        /// The date time format from UI
        /// </summary>
        private const string DateTimeFormat = "ddd MMM dd yyyy hh:mm:ss"; // string format from UI

        /// <summary>
        /// The date reg exp pattern
        /// </summary>
        private const string DateRegExpPattern = @"[A-Za-z0-9 ]+ [\d]{2}:[\d]{2}:[\d]{2}";
        
        /// <summary>
        /// Sets the filtering.
        /// </summary>
        /// <param name="filterField">The filter field.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="query">The query.</param>
        /// <returns>IQueryable{BonusAggregate}.</returns>
        public IQueryable<BonusAggregate> BuildFilter(string filterField, string filterValue, IQueryable<BonusAggregate> query)
        {
            switch (filterField)
            {
                case "EmployeeLastName":
                    query = query.Where(x => x.Employee.LastName.Contains(filterValue));
                    break;

                case "Date":
                    query = FormQueryWithDateFiltration(filterValue, query);
                    break;

                case "Amount":
//                    decimal amountValue = decimal.Parse(filterValue, CultureInfo.CreateSpecificCulture("en-GB"));
//                    query = query.Where(x => x.Amount == amountValue);

                    query = FormQueryWithAmountFiltration(filterValue, query);
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
                    DateTime dlc = GetDateFromFilterValue(filterValue);
                    query = query.Where(x => (x.Date.Year == dlc.Year &&
                                              x.Date.Month == dlc.Month &&
                                              x.Date.Day == dlc.Day));
                    break;
            }

            return query;
        }

        /// <summary>
        /// Forms the date filtered request.
        /// </summary>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        private static IQueryable<BonusAggregate> FormQueryWithDateFiltration(string filterValue,
                                                                              IQueryable<BonusAggregate> query)
        {
            string[] dateFilter = filterValue.Split(';');

            var fromDate = GetDateFromFilterValue(dateFilter[0]);

            DateTime toDate = DateTime.MinValue;
            if (dateFilter.Count() == 2)
                toDate = GetDateFromFilterValue(dateFilter[1]);


            if (fromDate != DateTime.MinValue && toDate == DateTime.MinValue)
            {
                query = query.Where(x => x.Date >= fromDate);
            }
            else if (fromDate == DateTime.MinValue && toDate != DateTime.MinValue)
            {
                toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day, 23, 59, 59); // until the end of a day
                query = query.Where(x => x.Date <= toDate);
            }
            else if (fromDate != DateTime.MinValue && toDate != DateTime.MinValue)
            {
                toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day, 23, 59, 59);
                query = query.Where(x => x.Date >= fromDate && x.Date <= toDate);
            }

            return query;
        }

        /// <summary>
        /// Gets the date from filter value.
        /// </summary>
        /// <param name="valueFromFilter">The value from filter.</param>
        /// <returns></returns>
        private static DateTime GetDateFromFilterValue(string valueFromFilter)
        {
            DateTime extractingDate = DateTime.MinValue;
            if (!string.IsNullOrEmpty(valueFromFilter.Trim()))
            {
                var pattern = new Regex(DateRegExpPattern);
                Match match = pattern.Match(valueFromFilter);

                string dateString = match.Value.Trim();
                extractingDate = DateTime.ParseExact(dateString, DateTimeFormat, CultureInfo.InvariantCulture);
            }
            return extractingDate;
        }


        /// <summary>
        /// Gets the amount from filter value.
        /// </summary>
        /// <param name="valueFromFilter">The value from filter.</param>
        /// <returns></returns>
        private static decimal GetAmountFromFilterValue(string valueFromFilter)
        {
            decimal amount = 0;
            if (!string.IsNullOrEmpty(valueFromFilter.Trim()))
            {
                amount = decimal.Parse(valueFromFilter, CultureInfo.CreateSpecificCulture("en-GB"));
            }

            return amount;
        }


        /// <summary>
        /// Forms the query with amount filtration.
        /// </summary>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        private static IQueryable<BonusAggregate> FormQueryWithAmountFiltration(string filterValue,
                                                                              IQueryable<BonusAggregate> query)
        {
            string[] amountFilter = filterValue.Split(';');

            var fromAmount = GetAmountFromFilterValue(amountFilter[0]);

            decimal toAmount = 0;
            if (amountFilter.Count() == 2)
                toAmount = GetAmountFromFilterValue(amountFilter[1]);


            if (fromAmount != 0 && toAmount == 0)
            {
                query = query.Where(x => x.Amount >= fromAmount);
            }
            else if (fromAmount == 0 && toAmount != 0)
            {
                query = query.Where(x => x.Amount <= toAmount);
            }
            else if (fromAmount != 0 && toAmount != 0)
            {
                query = query.Where(x => x.Amount >= fromAmount && x.Amount <= toAmount);
            }

            return query;
        }
    }
}