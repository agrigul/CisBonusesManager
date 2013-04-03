using System;

namespace Web.Models
{
    /// <summary>
    /// Class Bonus. Represents an entity from vwBonuses
    /// </summary>
    public class Bonus
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the employee.
        /// </summary>
        /// <value>The employee.</value>
        public virtual Employee Employee { get; set; }

        /// <summary>
        /// Gets the name of the employee user.
        /// </summary>
        /// <value>The name of the employee user.</value>
        public string EmployeeUserName
        {
            get
            {
                return Employee == null ? "" : Employee.UserName;
            }
        }

        /// <summary>
        /// Gets the last name of the employee.
        /// </summary>
        /// <value>The last name of the employee.</value>
        public string EmployeeLastName
        {
            get
            {
                return Employee == null ? "" : Employee.LastName;
            }
        }

        /// <summary>
        /// Gets the employee last name (ukr).
        /// </summary>
        /// <value>The employee last name (ukr)</value>
        public string EmployeeLastNameUkr
        {
            get
            {
                return Employee == null ? "" : Employee.LastNameUkr;
            }
        }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>The amount.</value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the comment. Max length 255
        /// </summary>
        /// <value>The comment.</value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the ulc. Max length is 20
        /// </summary>
        /// <value>The ulc.</value>
        public string Ulc { get; set; }

        /// <summary>
        /// Gets or sets the DLC.
        /// </summary>
        /// <value>The DLC.</value>
        public DateTime Dlc { get; set; }
    }
}