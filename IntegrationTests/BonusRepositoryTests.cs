using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Infrastructure.Repository;
using Web.Models;
using Web.Models.Repositories;

namespace IntegrationTests
{
    /// <summary>
    /// Tests for operations of BonusRepositoryTests over vwBonuses view
    /// </summary>
    [TestClass]
    public class BonusRepositoryTests
    {
        /// <summary>
        /// The repository of bonuses
        /// </summary>
        private IRepository<Bonus> repository;
        
        /// <summary>
        /// Tests the initialization.
        /// </summary>
        [TestInitialize]
        public void TestInitilalization()
        {
            repository = new BonusesRepository();
        }

        /// <summary>
        /// Tests the dispose.
        /// </summary>
        [TestCleanup]
        public void TestClean()
        {
            if (repository != null)
                repository.Dispose();

            repository = null;
        }

        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        
        [TestMethod]
        [Description("Checks that repository can select entities form database")]
        public void GetFirstBonus_noParams_bonuses()
        {
            IList<Bonus> bonuses;

            using (repository)
            {
                bonuses = repository.FindAll();
            }

            Assert.IsNotNull(bonuses);
            Assert.AreNotEqual(0, bonuses.Count);
        }

        [TestMethod]
        [Description("Checks that employees correctly mapped to bonuses")]
        public void GetFirstBonus_noParams_employeeExists()
        {
            Bonus bonus;

            using (repository)
            {
                bonus = repository.FindAll().First();
            }

            Assert.IsNotNull(bonus.Employee);
        }
    }
}
