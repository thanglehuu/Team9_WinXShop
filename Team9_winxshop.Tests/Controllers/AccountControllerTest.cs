using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Team9_winxshop.Controllers;

namespace Team9_winxshop.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void TestLogin()
        {
            var controller = new AccountController();
            var result = controller.Login("/Account/Login") as ViewResult;
            
            Assert.IsNotNull(result);
        }
    }
}
