using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        /// The employeeRepository of Employees
        /// </summary>
        private IRepository<Employee> employeeRepository;

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
//            if (employeeRepository != null)
//                employeeRepository.Dispose();

            employeeRepository = null;
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
        [Description("Repository can select entities form database")]
        public void GetFirstEmployee_noParams_employee()
        {
            Employee employee;


            using (var dbContext = new DatabaseContext())
            {
                employeeRepository = new EmployeesRepository(dbContext);
                employee = employeeRepository.FindAll().FirstOrDefault();
            }

            Assert.IsNotNull(employee);
        }


        [TestMethod]
        [Description("Repository can select entities form database by list of ids")]
        public void GetEmployees_ListOfIds_ListOfEmployees()
        {
            IList<Employee> employee;

            using (var dbContext = new DatabaseContext())
            {
                var repository = new EmployeesRepository(dbContext);
                var ids = new int[5] { 1, 2, 3, 4, 5 };
                employee = repository.GetByIdList(ids);
            }

            Assert.IsTrue(employee.Count > 1);
        }


        [TestMethod]
        [Description("Repository can select entities by last name")]
        public void FindByFilter_FilterString_ListOfEmployees()
        {
            IList<Employee> employee;
            string lastNameFilter = "Aleksandr";


            using (var dbContext = new DatabaseContext())
            {
                var repository = new EmployeesRepository(dbContext);
                employee = repository.FindByLastName(lastNameFilter);
            }
            
            Assert.IsTrue(employee.All(x => x.LastName.Contains(lastNameFilter)));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        [Description("Repository don't save employees in database")]
        public void Save_noParams_exception()
        {
            using (var dbContext = new DatabaseContext())
            {
                employeeRepository = new EmployeesRepository(dbContext);
                employeeRepository.Save(new Employee("", "", ""));
            }
        }
    }
}
