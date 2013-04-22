using System;
using Web.Infrastructure.Repository;
using Web.Models.Bonuses;
using Web.Models.Employees;

namespace Web.Models.Factories
{
    /// <summary>
    /// BonusFactory create new bonus aggreagte
    /// </summary>
    public class BonusFactory
    {
        /// <summary>
        /// The employees repository
        /// </summary>
        private IRepository<Employee> EmployeesRepository { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BonusFactory"/> class.
        /// </summary>
        /// <param name="employeesRepository">The employees repository.</param>
        /// <exception cref="System.ArgumentNullException">employeesRepository</exception>
        public BonusFactory(IRepository<Employee>  employeesRepository)
        {
            if(employeesRepository == null)
                throw new ArgumentNullException("BonusFactory", "Employees repository cann not be null");

            EmployeesRepository = employeesRepository;

        }
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
            return new BonusAggregate(employee, date, amount, comment, isActive, 0);
        }


        /// <summary>
        /// Creates the specified bonus aggregate from DTO object.
        /// </summary>
        /// <param name="bonusDto">The bonus DTO object.</param>
        /// <returns>
        /// BonusAggregate.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">Create;Can not create new BonusAggregate from null Dto object</exception>
        public BonusAggregate Create(BonusDto bonusDto)
        {
            if (bonusDto == null)
                throw new ArgumentNullException("Create", "can not create new BonusAggregate from null DTO object");

            Employee employee = EmployeesRepository.GetById(bonusDto.EmployeeId);
            
            
            return new BonusAggregate(employee, 
                                        bonusDto.Date, 
                                        bonusDto.Amount, 
                                        bonusDto.Comment, 
                                        bonusDto.IsActive, 
                                        bonusDto.BonusId);
        }
    }
}