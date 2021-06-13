using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Team9_winxshop.Models;

namespace Team9_winxshop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SanPhamsController : Controller
    {
        private CT25Team19Entities db = new CT25Team19Entities();

        // GET: SanPhams
        public ActionResult Index()
        {
            var model = db.SanPhams.ToList();
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Search(string keyword)
        {
            if( keyword.Trim(' ') == "")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var model = db.SanPhams.ToList();
                model = model.Where(p => p.TenSP.ToLower().Contains(keyword.ToLower())
                                    || p.Mau.ToLower().Contains(keyword.ToLower())
                                    || p.MaLoaiSP.ToLower().Contains(keyword.ToLower())
                                    || p.LoaiSanPham.TenLoaiSP.ToLower().Contains(keyword.ToLower())).ToList();
                ViewBag.Keyword = keyword;
                return View("Index2", model);
            }
        }

        public ActionResult Search2(string keyword)
        {
            var model = db.SanPhams.ToList();
            model = model.Where(p => p.TenSP.ToLower().Contains(keyword.ToLower())
                                || p.MaSP.ToLower().Contains(keyword.ToLower())
                                || p.LoaiSanPham.TenLoaiSP.ToLower().Contains(keyword.ToLower())).ToList();
            ViewBag.Keyword = keyword;
            return View("Index", model);

        }

        [AllowAnonymous]
        // for Customer
        public ActionResult Index2()
        {
            var model = db.SanPhams.ToList();
            return View(model);
        }

        [AllowAnonymous]
        // GET: SanPhams/Details/5
        public ActionResult Details(string id)
        {
            var model = db.SanPhams.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams, "MaLoaiSP", "TenLoaiSP");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SanPham model)
        {
            ValidateProduct(model);
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        private void ValidateProduct(SanPham sanPham)
        {
            if(sanPham.DonGia < 0)
            {
                ModelState.AddModelError("DonGia", "Đơn giá phải lớn hơn 0");
            }
            if(sanPham.SoLuong < 0)
            {
                ModelState.AddModelError("SoLuong", "Số lượng phải lớn hơn 0");
            }
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(string id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SanPham sanPham)
        {
            ValidateProduct(sanPham);
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(string id)
        {
            var model = db.SanPhams.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var model = db.SanPhams.Find(id);
            db.SanPhams.Remove(model);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
