using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Infrastructure.Repository;
using Web.Models;

namespace IntegrationTests
{
    /// <summary>
    /// Tests for operations of EmployeeRepository over vwEmployeesLookup view
    /// </summary>
    [TestClass]
    public class EmployeeRepositoryTests
    {
        public EmployeeRepositoryTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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
        public void GetFirstEmployee_noParams_employee()
        {
            Employee employee;

            using (var context = new BonusesDbContext())
            {
                employee = context.Employees.FirstOrDefault();
            }

            Assert.IsNotNull(employee);
        }
    }
}
