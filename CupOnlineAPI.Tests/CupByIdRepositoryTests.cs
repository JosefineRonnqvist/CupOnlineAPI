using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CupOnlineAPI.Context;
using CupOnlineAPI.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CupOnlineAPI.Tests
{
    [TestClass]
    public class CupByIdRepositoryTests
    {
        private readonly DapperContext context;

        [TestMethod]
        public void GetCupsTestAsync_returnCups()
        {
            var repo = new CupByIdRepository(context);
            var cup = repo.GetCupById(4838);
            Assert.AreEqual(cup.cup_name, "Test123");
        }
    }
}
