using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team9_winxshop.Models;

namespace Team9_winxshop.Controllers
{
    public class ChiTietDonHangsController : Controller
    {
        private CT25Team19Entities db = new CT25Team19Entities();
        [Authorize(Roles = "Admin")]
        // GET: ChiTietDonHangs
        public ActionResult Index()
        {
            var model = db.ChiTietDonHangs.ToList();
            return View(model);
        }

        public ActionResult Search(string keyword)
        {
            var model = db.ChiTietDonHangs.ToList();
            model = model.Where(p => p.MaDH.ToString().Contains(keyword)).ToList();
            ViewBag.Keyword = keyword;
            return View("Index", model);

        }

        // GET: ChiTietDonHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDonHang);
        }

        private void ValidateProduct(ChiTietDonHang sanPham)
        {
            if (sanPham.SoLuong < 0)
            {
                ModelState.AddModelError("SoLuong", "Số lượng phải lớn hơn 0");
            }
        }

        // GET: ChiTietDonHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "Email");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "MaLoaiSP");
            return View();
        }

        // POST: ChiTietDonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCTDH,MaDH,MaSP,SoLuong")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietDonHangs.Add(chiTietDonHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "Email", chiTietDonHang.MaDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "MaLoaiSP", chiTietDonHang.MaSP);
            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "Email", chiTietDonHang.MaDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "MaLoaiSP", chiTietDonHang.MaSP);
            return View(chiTietDonHang);
        }

        // POST: ChiTietDonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCTDH,MaDH,MaSP,SoLuong")] ChiTietDonHang chiTietDonHang)
        {
            ValidateProduct(chiTietDonHang);
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDonHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "Email", chiTietDonHang.MaDH);
            ViewBag.TenSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietDonHang.SanPham.TenSP);
            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            var model = db.ChiTietDonHangs.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: ChiTietDonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var chiTietDonHang = db.ChiTietDonHangs.Find(id);
            db.ChiTietDonHangs.Remove(chiTietDonHang);
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
