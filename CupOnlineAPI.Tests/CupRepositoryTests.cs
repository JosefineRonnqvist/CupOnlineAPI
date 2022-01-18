using CupOnlineAPI.Controllers;
using CupOnlineAPI.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CupOnlineAPI.Context;

namespace CupOnlineAPI.Tests
{
    [TestClass]
    public class CupRepositoryTests
    {
        private readonly DapperContext context;

        [TestMethod]
        public async Task GetCupsTestAsync_returnCups()
        {
            var repo = new CupRepository(context);
            var cups = await repo.GetCups(15);
            Assert.AreEqual(cups.Count(), 15);
        }

        [TestMethod]
        public async Task GetCupsTestAsync_returnZero()
        {
            var repo = new CupRepository(context);
            var cups = await repo.GetCups(0);
            Assert.AreEqual(cups.Count(), 0);
        }

        [TestMethod]
        public async Task GetCupsTestAsync_returnnull()
        {
            var repo = new CupRepository(context);
            var cups = await repo.GetCups(null);
            Assert.IsNull(cups);
        }

        [TestMethod]
        public async Task GetComing_returnCups()
        {
            var repo = new CupRepository(context);
            var cups = await repo.GetComing(15);
            Assert.AreEqual(cups.Count(), 15);
        }

        [TestMethod]
        public async Task GetComingNull_returnCups()
        {
            var repo = new CupRepository(context);
            var cups = await repo.GetComing(null);
            Assert.AreEqual(cups.Count(), 15);
        }
    }
}
