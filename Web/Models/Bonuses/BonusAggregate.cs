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
        public int EmployeeId { get; private set; }

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
                    throw new ArgumentNullException("Employee", "Employee can not be null");

                employee = value;
                EmployeeId = value.EmployeeId;
            }
        }

        /// <summary>
        /// Gets the name of the employee user.
        /// </summary>
        /// <value>The name of the employee user.</value>
        [Obsolete]
        private string EmployeeUserName
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
        [Obsolete]
        private string EmployeeLastNameUkr
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
        public BonusAggregate()
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
                                bool isActive, 
                                int bonusId)
        {
            if (employee == null)
                throw new ArgumentNullException("Bonus", "Employee can not be null to create bonus instance");
            Employee = employee;
            
            SetBonusProperties(date, amount, comment, isActive, bonusId);
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BonusAggregate"/> class.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="date">The date.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <param name="bonusId">The bonus id.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Bonus;Employee's id can not be zero or negative to create bonus instance</exception>
        internal BonusAggregate(int employeeId,
                                DateTime date,
                                decimal amount,
                                string comment,
                                bool isActive,
                                int bonusId)
        {
            if (employeeId <= 0)
                throw new ArgumentOutOfRangeException("Bonus", "Employee's id can not be zero or negative to create bonus instance");
            
            EmployeeId = employeeId;

            SetBonusProperties(date, amount, comment, isActive, bonusId);
        }


        /// <summary>
        /// Sets the bonus properties.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <param name="bonusId">The bonus id.</param>
        private void SetBonusProperties(DateTime date, decimal amount, string comment, bool isActive, int bonusId)
        {
            BonusId = bonusId;
            Date = date;
            Amount = amount;
            Comment = comment;
            IsActive = isActive;
            Ulc = "";
        }
    }
}