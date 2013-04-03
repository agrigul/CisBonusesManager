﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Web.Controllers;
using Web.Infrastructure.Repository;
using Web.Models;
using Web.Models.Bonuses;

namespace Web.Tests.Controllers.BonusesControll
{

    /// <summary>
    /// Test class of BonusesController
    /// </summary>
    [TestClass]
    public class BaseBonusesTest
    {
        /// <summary>
        /// The mocked repository of bonuses
        /// </summary>
        protected Mock<IRepository<BonusAggregate>> RepositoryMock;
        protected Mock<IRepository<BonusAggregate>> RepositoryWithExceptionMock;

        /// <summary>
        /// The prepared bonuses for test
        /// </summary>
        protected static readonly PagedResponse<BonusAggregate> PreparedBonusesForTest = CreateMockBonuses();

        /// <summary>
        /// The controller
        /// </summary>
        protected BonusesController Controller;

        /// <summary>
        /// Tests the initialization.
        /// </summary>
        [TestInitialize]
        public void TestInitilalization()
        {
            RepositoryMock = new Mock<IRepository<BonusAggregate>>();
            RepositoryMock.Setup(x => x.FindAllWithPaging(0, 2)).Returns(PreparedBonusesForTest);


            RepositoryWithExceptionMock = new Mock<IRepository<BonusAggregate>>();
            RepositoryWithExceptionMock.Setup(x => x.FindAllWithPaging(It.IsAny<int>(), -1))
                .Throws<ArgumentOutOfRangeException>();

            RepositoryWithExceptionMock.Setup(x => x.FindAllWithPaging(It.IsAny<int>(), 0))
                .Returns(() => new PagedResponse<BonusAggregate>(null, 0));
        }

        /// <summary>
        /// Tests the dispose.
        /// </summary>
        [TestCleanup]
        public void TestClean()
        {
            RepositoryMock = null;
        }

        /// <summary>
        /// Creates the bonuses mock.
        /// </summary>
        private static PagedResponse<BonusAggregate> CreateMockBonuses()
        {
            var factory = new BonusFactory();
            var bonuses = new List<BonusAggregate>
                                    {
                                        factory.Create(new Employee("name1", "lastname1", "ukr1"), DateTime.Now, 100),
                                        factory.Create(new Employee("name2", "lastname2", "ukr2"), DateTime.Now, 200)
                                    };

            return new PagedResponse<BonusAggregate>(bonuses, 2);
        }
    }
}
