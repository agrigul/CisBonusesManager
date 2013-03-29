using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Infrastructure.Repository;
using Web.Models;
using Web.Models.Bonuses;

namespace IntegrationTests
{
    /// <summary>
    /// Tests for operations of BonusRepositoryTests over vwBonuses view
    /// </summary>
    [TestClass]
    public class BonusRepositoryTests
    {
        /// <summary>
        /// The bonusRepository of bonuses
        /// </summary>
        private IRepository<BonusAggregate> bonusRepository;

        /// <summary>
        /// The employee repository
        /// </summary>
        private IRepository<Employee> employeeRepository;

        /// <summary>
        /// The bonus factory
        /// </summary>
        private readonly BonusFactory bonusFactory = new BonusFactory();

        /// <summary>
        /// Tests the initialization.
        /// </summary>
        [TestInitialize]
        public void TestInitilalization()
        {
        }

        /// <summary>
        /// Tests the dispose.
        /// </summary>
        [TestCleanup]
        public void TestClean()
        {
            if (bonusRepository != null)
                bonusRepository.Dispose();
            if (employeeRepository != null)
                employeeRepository.Dispose();

            bonusRepository = null;
            employeeRepository = null;
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
        [Description("Checks that bonusRepository can select entities form database")]
        public void FindAll_noParams_SomeBonuses()
        {
            IList<BonusAggregate> bonuses;

            using (bonusRepository = new BonusesRepository())
            {
                bonuses = bonusRepository.FindAll();
            }

            Assert.IsNotNull(bonuses);
            Assert.AreNotEqual(0, bonuses.Count);
        }

        [TestMethod]
        [Description("Checks that employees correctly mapped to bonuses")]
        public void FindAll_NoParams_EmployeeExists()
        {
            BonusAggregate bonusAggregate;

            using (bonusRepository = new BonusesRepository())
            {
                bonusAggregate = bonusRepository.FindAll().First();
            }

            Assert.IsNotNull(bonusAggregate.Employee);
        }

        [TestMethod]
        [Description("Checks that list of bonuses can be added to database")]
        public void Save_BonusesList_2BonusesAdded()
        {
            int numberOfItemsBeforSave;

            var bonusesList = new List<BonusAggregate> 
            { 
              bonusFactory.Create(GetEmployeeById(4), DateTime.Now, 100), 
              bonusFactory.Create(GetEmployeeById(5), DateTime.Now, 90)
            };

            
            int numberOfCurrentBonuses;
            using (bonusRepository = new BonusesRepository())
            {
                numberOfItemsBeforSave = bonusRepository.FindAll().Count();
                bonusRepository.Save(bonusesList);
                numberOfCurrentBonuses = bonusRepository.FindAll().Count();
            }

            Assert.AreEqual(numberOfCurrentBonuses - 2, numberOfItemsBeforSave);
        }

        [TestMethod]
        [Description("Checks that list of bonuses can be updated in database")]
        public void Save_BonusesList_2BonusesUpdated()
        {
            IList<BonusAggregate> bonusesToUpdate;
            IList<BonusAggregate> updatedBonuses = new List<BonusAggregate>();
            var bonusesIds = new int[2];
            
            string newComment = "comment on" + DateTime.Now;
            using (bonusRepository = new BonusesRepository())
            {
                bonusesToUpdate = bonusRepository.FindAll().Take(2).ToList();
                bonusesToUpdate[0].Comment = newComment;
                bonusesToUpdate[1].Comment = newComment;

                bonusesIds[0] = bonusesToUpdate[0].BonusId;
                bonusesIds[1] = bonusesToUpdate[1].BonusId;
                bonusRepository.Save(bonusesToUpdate);
            }

            using (bonusRepository = new BonusesRepository())
            {
                updatedBonuses.Add(bonusRepository.GetById(bonusesIds[0]));
                updatedBonuses.Add(bonusRepository.GetById(bonusesIds[1]));
            }

            Assert.AreEqual(updatedBonuses[0].Comment, newComment);
            Assert.AreEqual(updatedBonuses[1].Comment, newComment);
        }



        /// <summary>
        /// Gets the employee by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Employee.</returns>
        private Employee GetEmployeeById(int id = 1)
        {
            Employee employee;

            using (employeeRepository = new EmployeesRepository())
            {
                employee = employeeRepository.GetById(id);
            }

            return employee;
        }
    }
}
