using System;
using System.Collections.Generic;

namespace Web.Models.Repositories
{
    /// <summary>
    /// Interface for repository
    /// </summary>
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Finds all bonuses.
        /// </summary>
        /// <returns>IEnumerable{Bonus}.</returns>
        IList<T> FindAll();

        /// <summary>
        /// Gets bonus by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Bonus.</returns>
        T GetById(int id);
    }
}