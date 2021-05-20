using System;
using System.Linq;
using Team9_winxshop.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using Team9_winxshop.Models;
using System.Transactions;
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

        [TestMethod]
        public void TestCreateG()
        {
            var controller = new SanPhamsController();

            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCreateP()
        {
            var rand = new Random();
            var product = new SanPham
            {
                TenSP = rand.NextDouble().ToString(),
                SoLuong = -rand.Next(),
                DonGia = -rand.Next()
            };

            var controller = new SanPhamsController();
            var result0 = controller.Create(product, null) as ViewResult;

            Assert.IsNotNull(result0);
            Assert.AreEqual("Đơn giá phải lớn hơn 0", controller.ModelState["DonGia"].Errors[0].ErrorMessage);

            using (var scope = new TransactionScope())
            {
                var db = new CT25Team19Entities();
                var entity = db.SanPhams.SingleOrDefault(p => p.TenSP == product.TenSP && p.MaLoaiSP == product.MaLoaiSP && p.Mau == product.Mau);
                Assert.IsNotNull(entity);
                Assert.AreEqual(product.DonGia, entity.DonGia);

            }
        }
    }
}
