using System;

namespace Web.Models.Bonuses
{
    public class BonusDto
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int BonusId { get;  set; }

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        /// <value>The name of the employee.</value>
        public string EmployeeLastName { get; set; }

        /// <summary>
        /// This key is used as foreign key for mapping instead of Employee.EmployeeId
        /// </summary>
        /// <value>The employee id.</value>
        public int EmployeeId { get; set; }
        
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
        public BonusDto()
        {
            //this constructor is for EF use only
        }

        /// <summary>
        /// Serializes the specified bonus.
        /// </summary>
        /// <param name="bonus">The bonus.</param>
        /// <returns>BonusDto.</returns>
        public static BonusDto Serialize(BonusAggregate bonus)
        {
            return new BonusDto
                       {
                           BonusId = bonus.BonusId,
                           EmployeeId = bonus.EmployeeId,
                           Amount = bonus.Amount,
                           Comment = bonus.Comment,
                           Date = bonus.Date,
                           IsActive = bonus.IsActive,
                           Dlc = bonus.Dlc,
                           Ulc = bonus.Ulc
                       };
        }
    }
}