using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;
using Web.Infrastructure.Repository;
using Web.Models;
using Web.Models.Repositories;

namespace Web.Tests.Controllers
{
    [TestClass]
    public class BonusesControllTest
    {
        [TestMethod]
        [Description("Controller can return one or more bonus items")]
        public void Index_NoParams_MoreThanOneItem()
        {
            IBonusesRepository repository = new BonusesRepository();
            var controller = new BonusesController(repository);
            var result = controller.Index() as ViewResult;
            var bonuses = result.Model as IList<Bonus>;
            Assert.AreNotEqual(0, bonuses.Count);
        }
    }
}
