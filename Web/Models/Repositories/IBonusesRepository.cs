using System;
using System.Collections.Generic;

namespace Web.Models.Repositories
{
    /// <summary>
    /// Interface for repository of bonuses
    /// </summary>
    public interface IBonusesRepository : IDisposable
    {
        /// <summary>
        /// Finds all bonuses.
        /// </summary>
        /// <returns>IEnumerable{Bonus}.</returns>
        IList<Bonus> FindAll();

        /// <summary>
        /// Gets bonus by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Bonus.</returns>
        Bonus GetById(int id);
    }
}