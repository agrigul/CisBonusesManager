using System;

namespace Web.Models.Bonuses
{
    /// <summary>
    /// Class BonusAggregate. Represents an entity from vwBonuses
    /// </summary>
    public class BonusAggregate
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int BonusId { get; private set; }

        /// <summary>
        /// This key is used as foreign key for mapping instead of Employee.EmployeeId
        /// </summary>
        /// <value>The employee id.</value>
        public int EmployeeId { get; set; }

        /// <summary>
        /// The employee
        /// </summary>
        private Employee employee;

        /// <summary>
        /// Gets or sets the employee.
        /// </summary>
        /// <value>The employee.</value>
        public Employee Employee
        {
            get { return employee; }
            set{
                if(value == null)
                    throw new ArgumentNullException("Employee", "Employee can't be null");

                employee = value;
                EmployeeId = value.EmployeeId;
            }
        }

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
        public string Ulc { get; private set; }

        /// <summary>
        /// Gets or sets the DLC.
        /// </summary>
        /// <value>The DLC.</value>
        public DateTime Dlc { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BonusAggregate"/> class.
        /// </summary>
        protected BonusAggregate()
        {
            //this constructor is for EF use only
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BonusAggregate"/> class.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <param name="date">The date.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        internal BonusAggregate(Employee employee,
                                DateTime date,
                                decimal amount,
                                string comment,
                                bool isActive)
        {
            if (employee == null)
                throw new ArgumentNullException("Bonus", "Employee cant be null to create bonus instance");

            Employee = employee;
            Date = date;
            Amount = amount;
            Comment = comment;
            IsActive = isActive;
            Ulc = "";
        }
    }
}