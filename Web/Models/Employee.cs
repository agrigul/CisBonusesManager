namespace Web.Models
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
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name. Max length 10
        /// </summary>
        /// <value>The name.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the last name. Max length 250
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the last name ukr. Max length 250
        /// </summary>
        /// <value>The last name ukr.</value>
        public string LastNameUkr { get; set; }


//        /// <summary>
//        /// Gets or sets the bonus id.
//        /// </summary>
//        /// <value>The bonus id.</value>
//        // public virtual Bonus Bonus { get; set; } TODO: add relations with bonuses
    }
}