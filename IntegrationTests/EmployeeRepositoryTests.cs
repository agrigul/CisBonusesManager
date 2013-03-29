﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Infrastructure.Repository;
using Web.Models.Bonuses;


namespace IntegrationTests
{
    /// <summary>
    /// Tests for operations of EmployeeRepository over vwEmployeesLookup view
    /// </summary>
    [TestClass]
    public class EmployeeRepositoryTests
    {
        /// <summary>
        /// The repository of Employees
        /// </summary>
        private IRepository<Employee> repository;

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
            if (repository != null)
                repository.Dispose();

            repository = null;
        }

        /// <summary>
        /// The test context instance
        /// </summary>
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
        public void GetFirstEmployee_noParams_employee()
        {
            Employee employee;

            using (repository = new EmployeesRepository())
            {
                employee = repository.FindAll().FirstOrDefault();
            }

            Assert.IsNotNull(employee);
        }


        [TestMethod]
        [Description("Checks that repository can select entities form database by list of ids")]
        public void GetEmployees_ListOfIds_ListOfEmployees()
        {
            IList<Employee> employee;

            using (var repository = new EmployeesRepository())
            {
                var ids = new int[5] {1, 2, 3, 4, 5};
                employee = repository.GetByIdList(ids);
            }

            Assert.IsTrue(employee.Count > 1);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        [Description("Checks that repository don't save employees in database")]
        public void Save_noParams_exception()
        {
            using (repository = new EmployeesRepository())
            {
                repository.Save(new Employee("","",""));
            }
        }
    }
}
