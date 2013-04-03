using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;

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
        public void GetPagedJsonBonuses_Skip0Take2_TwoBonuses()
        {
            dynamic bonuses;
            using (Controller = new BonusesController(RepositoryMock.Object))
            {
                bonuses = Controller.GetPagedJsonBonuses(2, 0).Data;
            }
            Assert.AreEqual(2, bonuses.TotalCount);
        }


        [TestMethod]
        [Description("Controller can return a bonus and it's employee's last name")]
        public void GetPagedJsonBonuses_Skip0Take2_EmployeeLastName()
        {
            dynamic bonuses;
            using (Controller = new BonusesController(RepositoryMock.Object))
            {
                bonuses = Controller.GetPagedJsonBonuses(2,0).Data;
            }
            Assert.IsTrue(bonuses.Data[0].EmployeeLastName == "lastname1");
            
        }

        [TestMethod]
        [Description("Controller can return a bonus and it's employee's amount")]
        public void GetPagedJsonBonuses_Skip0Take2_Amount()
        {
            dynamic bonuses;
            using (Controller = new BonusesController(RepositoryMock.Object))
            {
                bonuses = Controller.GetPagedJsonBonuses(2, 0).Data;
            }
            Assert.IsTrue(bonuses.Data[0].Amount == 100);
        }

        [TestMethod]
        [Description("Controller can return a bonus and it's employee's date")]
        public void GetPagedJsonBonuses_Skip0Take2_Date()
        {
            dynamic bonuses;
            using (Controller = new BonusesController(RepositoryMock.Object))
            {
                bonuses = Controller.GetPagedJsonBonuses(2, 0).Data;
            }
            Assert.IsTrue(bonuses.Data[0].Date.Date ==  DateTime.Now.Date);
        }

        [TestMethod]
        [Description("Controller can return a bonus and it's employee's isActive param")]
        public void GetPagedJsonBonuses_Skip0Take2_IsActive()
        {
            dynamic bonuses;
            using (Controller = new BonusesController(RepositoryMock.Object))
            {
                bonuses = Controller.GetPagedJsonBonuses(2, 0).Data;
            }
            Assert.IsTrue(bonuses.Data[0].IsActive == false);
        }
    }
}
