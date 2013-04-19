using System;
using System.Collections.Specialized;
using System.Linq;

namespace Web.Models.ValueObjects
{
    /// <summary>
    /// A value object request which form params filtration and sorting
    /// </summary>
    public class FilteredRequest
    {
        /// <summary>
        /// Gets the sorting field name.
        /// </summary>
        /// <value>
        /// The sorting field.
        /// </value>
        public string SortingField { get; private set; }

        /// <summary>
        /// Gets the sort direction.
        /// </summary>
        /// <value>
        /// The sort direction.
        /// </value>
        public string SortDirection { get; private set; }

        /// <summary>
        /// Gets the filter field name.
        /// </summary>
        /// <value>
        /// The filter field.
        /// </value>
        public string FilterField { get; private set; }

        /// <summary>
        /// Gets the filter pattern.
        /// </summary>
        /// <value>
        /// The filter pattern.
        /// </value>
        public string FilterPattern { get; private set; }

        /// <summary>
        /// Gets the direction of sorting.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public SortingDirection Direction { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilteredRequest"/> class.
        /// </summary>
        /// <param name="requestParams">The request params.</param>
        public FilteredRequest(NameValueCollection requestParams)
        {

            if (requestParams != null)
            {
                SetSorting(requestParams);
                SetFilter(requestParams);
            }
        }

        /// <summary>
        /// Sets the sorting.
        /// </summary>
        /// <param name="requestParams">The request params.</param>
        private void SetSorting(NameValueCollection requestParams)
        {
            var itemIsInRequest = (from k in requestParams.AllKeys
                                   where k.Contains("sort[0]")
                                   select k).FirstOrDefault();

            if (itemIsInRequest != null)
            {
                SortingField = requestParams["sort[0][field]"];
                SortDirection = requestParams["sort[0][dir]"];
            }

            if (String.IsNullOrEmpty(SortDirection))
                Direction = SortingDirection.Desc;
            else
                Direction = SortDirection == "asc" ? SortingDirection.Asc : SortingDirection.Desc;
            
        }

        /// <summary>
        /// Sets the filter's field and pattern.
        /// </summary>
        /// <param name="requestParams">The request params.</param>
        private void SetFilter(NameValueCollection requestParams)
        {
            string itemIsInRequest = (from k in requestParams.AllKeys
                               where k.Contains("filter[filters][0]")
                               select k).FirstOrDefault();

            if (itemIsInRequest == null) return;

            FilterField = requestParams["filter[filters][0][field]"];
            FilterPattern = FilterBuilder.FormFilterValue(requestParams, FilterField);
        }
    }
}