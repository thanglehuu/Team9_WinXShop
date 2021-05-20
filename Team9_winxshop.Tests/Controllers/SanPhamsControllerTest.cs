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
            var result0 = controller.Create(product) as ViewResult;

            Assert.IsNotNull(result0);
            Assert.AreEqual("Đơn giá phải lớn hơn 0", controller.ModelState["DonGia"].Errors[0].ErrorMessage);
            Assert.AreEqual("Số lượng phải lớn hơn 0", controller.ModelState["SoLuong"].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void TestEditP()
        {
            var rand = new Random();
            var product = new SanPham
            {
                TenSP = rand.NextDouble().ToString(),
                SoLuong = -rand.Next(),
                DonGia = -rand.Next()
            };

            var controller = new SanPhamsController();
            var result0 = controller.Edit(product) as ViewResult;

            Assert.IsNotNull(result0);
            Assert.AreEqual("Đơn giá phải lớn hơn 0", controller.ModelState["DonGia"].Errors[0].ErrorMessage);
            Assert.AreEqual("Số lượng phải lớn hơn 0", controller.ModelState["SoLuong"].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void TestEditG()
        {
            var controller = new SanPhamsController();
            var result0 = controller.Edit(ToString()) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new CT25Team19Entities();
            var product = db.SanPhams.First();
            var result = controller.Edit(product.MaSP) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as SanPham;
            Assert.IsNotNull(model);
            Assert.AreEqual(product.MaSP, model.MaSP);
            Assert.AreEqual(product.TenSP, model.TenSP);
            Assert.AreEqual(product.SoLuong, model.SoLuong);
            Assert.AreEqual(product.MaLoaiSP, model.MaLoaiSP);
            Assert.AreEqual(product.Mau, model.Mau);
            Assert.AreEqual(product.HinhAnh, model.HinhAnh);
            Assert.AreEqual(product.Size, model.Size);
        }

        [TestMethod]
        public void TestDeleteG()
        {
            var controller = new SanPhamsController();
            var result0 = controller.Delete(ToString()) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new CT25Team19Entities();
            var product = db.SanPhams.First();
            var result = controller.Edit(product.MaSP) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as SanPham;
            Assert.IsNotNull(model);
            Assert.AreEqual(product.MaSP, model.MaSP);
            Assert.AreEqual(product.TenSP, model.TenSP);
            Assert.AreEqual(product.SoLuong, model.SoLuong);
            Assert.AreEqual(product.MaLoaiSP, model.MaLoaiSP);
            Assert.AreEqual(product.Mau, model.Mau);
            Assert.AreEqual(product.HinhAnh, model.HinhAnh);
            Assert.AreEqual(product.Size, model.Size);
        }
    }
}
