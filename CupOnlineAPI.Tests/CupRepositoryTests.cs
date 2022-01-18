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
        public void GetCupsTest()
        {
            var repo = new CupRepository(context);
            var cups = repo.GetCups(15);
            Assert.AreEqual()
        }
    }
}
