using System.Collections.Generic;

namespace Web.Models
{
    /// <summary>
    /// Class for response with paging
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResponse<T> where T : class
    {
        /// <summary>
        /// Total number ot Items
        /// </summary>
        /// <value>The size of the page.</value>
        public int TotalCount { get; private set; }

        /// <summary>
        /// Response data
        /// </summary>
        /// <value>The data.</value>
        public IList<T> Data { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedResponse{T}" /> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="totalCount">The total number of items in table</param>
        public PagedResponse(IList<T> data, int totalCount)
        {
            Data = data ?? new List<T>();
            TotalCount = totalCount;
        }
    }
}