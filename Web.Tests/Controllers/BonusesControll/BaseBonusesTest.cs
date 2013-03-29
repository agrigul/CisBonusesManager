using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Web.Controllers;
using Web.Infrastructure.Repository;
using Web.Models.Bonuses;

namespace Web.Tests.Controllers.BonusesControll
{

    /// <summary>
    /// Test class of BonusesController
    /// </summary>
    public class BaseBonusesTest
    {
        /// <summary>
        /// The mocked repository of bonuses
        /// </summary>
        protected Mock<IRepository<BonusAggregate>> RepositoryMock;

        /// <summary>
        /// The prepared bonuses for test
        /// </summary>
        protected static readonly IList<BonusAggregate> PreparedBonusesForTest = CreateMockBonuses();

        /// <summary>
        /// The controller
        /// </summary>
        protected BonusesController Controller;

        /// <summary>
        /// Tests the initialization.
        /// </summary>
        [TestInitialize]
        public void TestInitilalization()
        {
            RepositoryMock = new Mock<IRepository<BonusAggregate>>();
            RepositoryMock.Setup(x => x.FindAll()).Returns(PreparedBonusesForTest);

        }

        /// <summary>
        /// Tests the dispose.
        /// </summary>
        [TestCleanup]
        public void TestClean()
        {            
            RepositoryMock = null;
        }

        /// <summary>
        /// Creates the mock bonuses.
        /// </summary>
        private static IList<BonusAggregate> CreateMockBonuses()
        {
            var factory = new BonusFactory();
            return new List<BonusAggregate>
                                    {
                                        factory.Create(new Employee("name1", "lastname1", "ukr1"), DateTime.Now, 100),
                                        factory.Create(new Employee("name2", "lastname2", "ukr2"), DateTime.Now, 200)
                                    };
        }
    }
}
