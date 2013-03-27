using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;
using Web.Models;
using Web.Models.Repositories;
using Moq;

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
        private Mock<IRepository<Bonus>> repositoryMock;

        /// <summary>
        /// The prepared bonuses for test
        /// </summary>
        private static readonly IList<Bonus> PreparedBonusesForTest = CreateMockBonuses();

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
            repositoryMock = new Mock<IRepository<Bonus>>();
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
            var bonuses = result.Model as IList<Bonus>;

            Assert.AreEqual(2, bonuses.Count);
        }

        [TestMethod]
        [Description("Controller can return a bonus and it's employee's user name")]
        public void Index_NoParams_EmployeeUserName()
        {
            var result = controller.Index() as ViewResult;
            var bonuses = result.Model as IList<Bonus>;
            Assert.IsTrue(bonuses.Any(x => x.EmployeeUserName == "name1"));
        }


        [TestMethod]
        [Description("Controller can return a bonus and it's employee's last name")]
        public void Index_NoParams_EmployeeLastName()
        {
            var result = controller.Index() as ViewResult;
            var bonuses = result.Model as IList<Bonus>;
            Assert.IsTrue(bonuses.Any(x => x.EmployeeLastName == "lastname1"));
        }

        /// <summary>
        /// Creates the mock bonuses.
        /// </summary>
        private static IList<Bonus> CreateMockBonuses()
        {
            return new List<Bonus>
                                    {
                                        new Bonus
                                            {
                                                Id = 1,
                                                Amount = 100,
                                                Employee =
                                                    new Employee {Id = 11, UserName = "name1", LastName = "lastname1"}
                                            },
                                        new Bonus
                                            {
                                                Id = 2,
                                                Amount = 200,
                                                Employee =
                                                    new Employee {Id = 22, UserName = "name2", LastName = "lastname1"}
                                            }
                                    };
        }
    }
}
