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

        [TestMethod]
        public void TestDispose()
        {

        }

        [TestMethod]
        public void TestDetails()
        {
            var controller = new SanPhamsController();
            var result0 = controller.Details("") as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new CT25Team19Entities();
            var product = db.SanPhams.First();
            var result = controller.Details(product.MaSP) as ViewResult;
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
        public void TestSearch()
        {
            var db = new CT25Team19Entities();
            var products = db.SanPhams.ToList();
            var keyword = products.First().TenSP.Split().First();
            products = products.Where(p => p.TenSP.ToLower().Contains(keyword.ToLower())).ToList();

            var controller = new SanPhamsController();
            var result = controller.Search(keyword) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<SanPham>;
            Assert.IsNotNull(model);

            Assert.AreEqual("Index2", result.ViewName);
            Assert.AreEqual(products.Count(), model.Count);
            Assert.AreEqual(keyword, result.ViewData["keyword"]);
        }

        [TestMethod]
        public void TestSearch2()
        {
            var db = new CT25Team19Entities();
            var products = db.SanPhams.ToList();
            var keyword1 = products.First().TenSP.Split().First();
            var keyword2 = products.First().TenSP.Split().First();
            products = products.Where(p => p.TenSP.ToLower().Contains(keyword1.ToLower()) || p.MaSP.ToLower().Contains(keyword2.ToLower())).ToList();

            var controller = new SanPhamsController();
            var result = controller.Search(keyword1) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<SanPham>;
            Assert.IsNotNull(model);

            Assert.AreEqual("Index2", result.ViewName);
            Assert.AreEqual(products.Count(), model.Count);
            Assert.AreEqual(keyword1, result.ViewData["keyword"]);
            Assert.AreEqual(keyword2, result.ViewData["keyword"]);
        }
    }
}
