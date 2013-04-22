using System.Linq;
using Web.Models.Bonuses;
using Web.Models.ValueObjects;

namespace Web.Models.Factories
{
    /// <summary>
    /// Buildes a query with sorting
    /// </summary>
    public class BonusesSortingQueryBuilder
    {

        /// <summary>
        /// Adds sorting to query according to parameters from UI
        /// </summary>
        /// <param name="sortField">The sort field.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="query">The query to change.</param>
        /// <returns>IQueryable{BonusAggregate}.</returns>
        public IQueryable<BonusAggregate> BuildQuery(string sortField, SortingDirection sortDirection, IQueryable<BonusAggregate> query)
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
    }
}