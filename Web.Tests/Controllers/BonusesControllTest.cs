using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;
using Web.Infrastructure.Repository;
using Web.Models;
using Moq;
using Web.Models.Bonuses;

namespace Web.Tests.Controllers
{
    /// <summary>
    /// Bonuses controller test class
    /// </summary>
    [TestClass]
    public class BonusesControllTest
    {
        /// <summary>
        /// The mocked repository of bonuses
        /// </summary>
        private Mock<IRepository<BonusAggregate>> repositoryMock;

        /// <summary>
        /// The prepared bonuses for test
        /// </summary>
        private static readonly IList<BonusAggregate> PreparedBonusesForTest = CreateMockBonuses();

        /// <summary>
        /// The controller
        /// </summary>
        private BonusesController controller;

        /// <summary>
        /// Tests the initialization.
        /// </summary>
        [TestInitialize]
        public void TestInitilalization()
        {
            repositoryMock = new Mock<IRepository<BonusAggregate>>();
            repositoryMock.Setup(x => x.FindAll()).Returns(PreparedBonusesForTest);
            controller = new BonusesController(repositoryMock.Object);
        }

        /// <summary>
        /// Tests the dispose.
        /// </summary>
        [TestCleanup]
        public void TestClean()
        {
            repositoryMock = null;
        }

        [TestMethod]
        [Description("Controller can return one or more bonus items")]
        public void Index_NoParams_MoreThanOneItem()
        {
            var result = controller.Index() as ViewResult;
            var bonuses = result.Model as IList<BonusAggregate>;

            Assert.AreEqual(2, bonuses.Count);
        }

        [TestMethod]
        [Description("Controller can return a bonus and it's employee's user name")]
        public void Index_NoParams_EmployeeUserName()
        {
            var result = controller.Index() as ViewResult;
            var bonuses = result.Model as IList<BonusAggregate>;
            Assert.IsTrue(bonuses.Any(x => x.EmployeeUserName == "name1"));
        }


        [TestMethod]
        [Description("Controller can return a bonus and it's employee's last name")]
        public void Index_NoParams_EmployeeLastName()
        {
            var result = controller.Index() as ViewResult;
            var bonuses = result.Model as IList<BonusAggregate>;
            Assert.IsTrue(bonuses.Any(x => x.EmployeeLastName == "lastname1"));
        }

        /// <summary>
        /// Creates the mock bonuses.
        /// </summary>
        private static IList<BonusAggregate> CreateMockBonuses()
        {
            BonusFactory factory = new BonusFactory();
            factory.Create(new Employee("name1", "lastname1", "ukr1"), DateTime.Now, 100);
            return new List<BonusAggregate>
                                    {
                                        factory.Create(new Employee("name1", "lastname1", "ukr1"), DateTime.Now, 100),
                                        factory.Create(new Employee("name1", "lastname1", "ukr1"), DateTime.Now, 200)
                                    };
        }
    }
}
