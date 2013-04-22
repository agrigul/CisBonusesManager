using System.Collections.Generic;
using Web.Models.ValueObjects;

namespace Web.Infrastructure.Repository
{
    /// <summary>
    /// Interface for repository
    /// </summary>
    public interface IRepository<T>  where T : class
    {
        /// <summary>
        /// Finds all bonuses.
        /// </summary>
        /// <returns>IEnumerable{BonusAggregate}.</returns>
        IList<T> FindAll();

        /// <summary>
        /// Finds all using paging and sorting.
        /// </summary>
        /// <param name="skip">The skip parameter.</param>
        /// <param name="take">The take parameter.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="filterField">The filter field.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <returns>PagedResponse{`0}.</returns>
        PagedResponse<T> FindAll(int skip, 
                                    int take,
                                    string sortField,
                                    SortingDirection sortDirection,
                                    string filterField,
                                    string filterValue);

        /// <summary>
        /// Finds all items with paging.
        /// </summary>
        /// <param name="skip">Number of items to skip</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>PagedResponse{`0}.</returns>
        PagedResponse<T> FindAllWithPaging(int skip, int take);
        
        /// <summary>
        /// Gets bonus by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>BonusAggregate.</returns>
        T GetById(int id);

        /// <summary>
        /// Saves the specified list of items.
        /// </summary>
        /// <param name="list">The list of items.</param>
        void Save(IEnumerable<T> list);

        /// <summary>
        /// Saves the specified item.
        /// </summary>
        /// <param name="item">item.</param>
        void Save(T item);
    }
}