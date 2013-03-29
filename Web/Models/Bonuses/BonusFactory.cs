using System;

namespace Web.Models.Bonuses
{
    /// <summary>
    /// Class BonusFactory
    /// </summary>
    public class BonusFactory
    {
        /// <summary>
        /// Creates the specified employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <param name="date">The date.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns>BonusAggregate.</returns>
        public BonusAggregate Create(Employee employee, 
                                    DateTime date, 
                                    decimal amount, 
                                    string comment = "", 
                                    bool isActive = false)
        {
            return new BonusAggregate(employee, date, amount, comment, isActive);
        }
    }
}