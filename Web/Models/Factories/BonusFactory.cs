﻿using System;
using Web.Infrastructure.Repository;
using Web.Models.Bonuses;

namespace Web.Models.Factories
{
    /// <summary>
    /// BonusFactory create new bonus aggreagte
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
        public BonusAggregate Create(Employee.Employee employee, 
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

            Employee.Employee employee;
            using (var dbContext = new DatabaseContext())
            {
                var repository = new EmployeesRepository(dbContext);
                employee = repository.GetById(bonusDto.EmployeeId);
            }
            
            return new BonusAggregate(employee, 
                                        bonusDto.Date, 
                                        bonusDto.Amount, 
                                        bonusDto.Comment, 
                                        bonusDto.IsActive, 
                                        bonusDto.BonusId);
        }
    }
}