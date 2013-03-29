using System;
using System.Collections.Generic;

namespace Web.Infrastructure.Repository
{
    /// <summary>
    /// Interface for repository
    /// </summary>
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Finds all bonuses.
        /// </summary>
        /// <returns>IEnumerable{BonusAggregate}.</returns>
        IList<T> FindAll();

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