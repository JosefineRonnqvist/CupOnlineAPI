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

        private CupRepository repo;   

        public CupRepositoryTests()
        {
            repo = new CupRepository(context);
        }

        [TestMethod]
        public async Task GetComing_returnCups()
        {
            var cups = await repo.GetComing(15,30);
            Assert.AreEqual(cups.Count(), 15);
        }

        [TestMethod]
        public async Task GetComingNull_returnCups()
        {
            var repo = new CupRepository(context);
            var cups = await repo.GetComing(null,30);
            Assert.AreEqual(cups.Count(), 0);
        }
    }
}
