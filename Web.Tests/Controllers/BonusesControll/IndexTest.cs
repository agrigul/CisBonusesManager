using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;
using Web.Models.Bonuses;

namespace Web.Tests.Controllers.BonusesControll
{
    /// <summary>
    ///Test class of Index method of BonusesControll
    /// </summary>
    [TestClass]
    public class IndexTest : BaseBonusesTest
    {
        
        [TestMethod]
        [Description("Controller can return one or more bonus items")]
        public void Index_NoParams_MoreThanOneItem()
        {
            IList<BonusAggregate> bonuses;
            using (Controller = new BonusesController(RepositoryMock.Object))
            {
                var result = Controller.Index() as ViewResult;
                bonuses = result.Model as IList<BonusAggregate>;
            }
            Assert.AreEqual(2, bonuses.Count);
        }


        [TestMethod]
        [Description("Controller can return a bonus and it's employee's last name")]
        public void Index_NoParams_EmployeeLastName()
        {
            IList<BonusAggregate> bonuses;
            using (Controller = new BonusesController(RepositoryMock.Object))
            {
                var result = Controller.Index() as ViewResult;
                 bonuses = result.Model as IList<BonusAggregate>;
            }

            Assert.IsTrue(bonuses.Any(x => x.EmployeeLastName == "lastname1"));
            
        }

        [TestMethod]
        [Description("Controller can return a bonus and it's employee's amount")]
        public void Index_NoParams_Amount()
        {
            IList<BonusAggregate> bonuses;
            using (Controller = new BonusesController(RepositoryMock.Object))
            {
                var result = Controller.Index() as ViewResult;
                bonuses = result.Model as IList<BonusAggregate>;
            }
            Assert.IsTrue(bonuses.Any(x => x.Amount == 100));
        }

        [TestMethod]
        [Description("Controller can return a bonus and it's employee's date")]
        public void Index_NoParams_Date()
        {
            IList<BonusAggregate> bonuses;
            using (Controller = new BonusesController(RepositoryMock.Object))
            {
                var result = Controller.Index() as ViewResult;
                bonuses = result.Model as IList<BonusAggregate>;
            }
            Assert.IsTrue(bonuses.Any(x => x.Date.Date == DateTime.Now.Date));
        }

        [TestMethod]
        [Description("Controller can return a bonus and it's employee's isActive param")]
        public void Index_NoParams_IsActive()
        {
            IList<BonusAggregate> bonuses;
            using (Controller = new BonusesController(RepositoryMock.Object))
            {
                var result = Controller.Index() as ViewResult;
                bonuses = result.Model as IList<BonusAggregate>;
            }
            Assert.IsTrue(bonuses.Any(x => x.IsActive == false));
        }
    }
}
