using System;
using System.Linq;
using Team9_winxshop.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using Team9_winxshop.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Team9_winxshop.Tests.Controllers
{
    [TestClass]
    public class SanPhamsControllerTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var controller = new SanPhamsController();

            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<SanPham>;
            Assert.IsNotNull(model);

            var db = new CT25Team19Entities();
            Assert.AreEqual(db.SanPhams.Count(), model.Count);
        }

        [TestMethod]
        public void TestIndex2()
        {
            var controller = new SanPhamsController();

            var result = controller.Index2() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<SanPham>;
            Assert.IsNotNull(model);

            var db = new CT25Team19Entities();
            Assert.AreEqual(db.SanPhams.Count(), model.Count);
        }
    }
}
