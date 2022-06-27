using CupOnlineAPI.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCupOnline
{
    [TestClass]
    public class PasswordTests
    {
        [TestMethod]
        public void HashMatchesPassword()
        {
            var password = "213sworD";
            var repo = new PasswordRepository();
            var hash = repo.HashPassword(password);

            var match = repo.VerifyPassword(password, hash);

            Assert.IsTrue(match);
        }
    }
}