using System.Collections.Generic;

namespace Web.Models.Bonuses
{
    /// <summary>
    /// Class Employee. Represents an entity from vwEmployeeLookup
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int EmployeeId { get; private set; }

        /// <summary>
        /// Gets or sets the name. Max length 10
        /// </summary>
        /// <value>The name.</value>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets or sets the last name. Max length 250
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets or sets the last name ukr. Max length 250
        /// </summary>
        /// <value>The last name ukr.</value>
        public string LastNameUkr { get;private set; }
        
        /// <summary>
        /// Gets or sets the bonuses list. Filed executed for one-to-many relations only
        /// </summary>
        /// <value>The bonuses list.</value>
        //public virtual IList<BonusAggregate> BonusesList { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// 
        /// </summary>
        protected Employee()
        {
            //this constructor is for EF use only
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="lastNameUkr">The last name ukr.</param>
        public Employee(string userName, string lastName, string lastNameUkr)
        {
            UserName = userName;
            LastName = lastName;
            LastNameUkr = lastNameUkr;
        }
    }
}