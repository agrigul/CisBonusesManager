using System;
using System.Collections.Specialized;

namespace Web.Models
{
    /// <summary>
    /// Build different filter values depending on request params
    /// </summary>
    public static class FilterBuilder
    {
        /// <summary>
        /// The greater or equal abriviation from UI
        /// </summary>
        private const string GreaterOrEqual = "gte";

        /// <summary>
        /// The less or equal abriviation from UI
        /// </summary>
        private const string LessOrEqual = "lte";

        /// <summary>
        /// Forms the filter value.
        /// </summary>
        /// <param name="requstParams">The requst params.</param>
        /// <param name="filterField">The filter field.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">FormFilterValue;requstParams can not be null</exception>
        public static string FormFilterValue(NameValueCollection requstParams, string filterField = null)
        {
            if(requstParams == null)
                throw new ArgumentNullException("FormFilterValue", "requstParams can not be null");

            string filterValue = requstParams["filter[filters][0][value]"];

            if (!string.IsNullOrEmpty(filterField) && 
                 filterField.Contains("Date"))
            {
                    filterValue = FormDateFilter(requstParams);
            }

            return filterValue;
        }

        /// <summary>
        /// Forms the date filter.
        /// </summary>
        /// <param name="requstParams">The requst params.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">FormFilterValue;requstParams can not be null</exception>
        private static string FormDateFilter(NameValueCollection requstParams)
        {
            if (requstParams == null)
                throw new ArgumentNullException("FormFilterValue", "requstParams can not be null");

            // date has two values to compare
            string firstOperator = requstParams["filter[filters][0][operator]"];
            string secondOperator = requstParams["filter[filters][1][operator]"];
            string firstFilterValue = requstParams["filter[filters][0][value]"];
            string secondFilterValue = requstParams["filter[filters][1][value]"];

            string finalFilterValue = "";
            if (firstOperator == GreaterOrEqual && secondOperator == LessOrEqual)
            {
                finalFilterValue = string.Format("{0} ; {1}", firstFilterValue,
                                                              secondFilterValue);
            }
            else if (firstOperator == LessOrEqual && secondOperator == GreaterOrEqual)
            {
                finalFilterValue = string.Format("{0} ; {1}",secondFilterValue, 
                                                             firstFilterValue);
            }
            else
            {
                finalFilterValue = FormDateWithOneParameter(secondFilterValue, 
                                                            firstFilterValue, 
                                                            firstOperator, 
                                                            secondOperator);
            }

            return finalFilterValue;
        }

        private static string FormDateWithOneParameter(string secondFilterValue, string firstFilterValue, string firstOperator,
                                                       string secondOperator)
        {
            string finalFilterValue = "";
            if (firstOperator == secondOperator)
                secondOperator = ""; // delete misunderstangig for sql query.

            if (firstOperator == GreaterOrEqual)
                finalFilterValue = string.Format("{0} ; ", firstFilterValue);
            if (firstOperator == LessOrEqual)
                finalFilterValue = string.Format(" ; {0} ", firstFilterValue);

            if (secondOperator == GreaterOrEqual)
                finalFilterValue = string.Format("{0} ; ", secondFilterValue);
            if (secondOperator == LessOrEqual)
                finalFilterValue = string.Format(" ; {0} ", secondFilterValue);

            return finalFilterValue;
        }
    }
}