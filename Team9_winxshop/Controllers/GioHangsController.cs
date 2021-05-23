using System;
using System.Collections;
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
    [Authorize]
    public class GioHangsController : Controller
    {
        private CT25Team19Entities db = new CT25Team19Entities();
        private List<ChiTietGioHang> ShoppingCart = null;

        public GioHangsController()
        {
            var session = System.Web.HttpContext.Current.Session;
            if(session["ShoppingCart"] != null)
            {
                ShoppingCart = session["ShoppingCart"] as List<ChiTietGioHang>;
            }
            else
            {
                ShoppingCart = new List<ChiTietGioHang>();
                session["ShoppingCart"] = ShoppingCart;
            }
        }

        // GET: GioHangs
        public ActionResult Index()
        {
            var hashtable = new Hashtable();
            foreach(var billdetail in ShoppingCart)
            {
                if (hashtable[billdetail.SanPham.MaSP] != null)
                {
                    (hashtable[billdetail.SanPham.MaSP] as ChiTietGioHang).SoLuong += billdetail.SoLuong;
                }
                else hashtable[billdetail.SanPham.MaSP] = billdetail;
            }

            ShoppingCart.Clear();
            foreach(ChiTietGioHang ctgh in hashtable.Values)
            {
                ShoppingCart.Add(ctgh);
            }

            return View(ShoppingCart);
        }


        // GET: GioHangs/Create
        [HttpPost]
        public ActionResult Create(string productId, int quantity)
        {
            var product = db.SanPhams.Find(productId);
            ShoppingCart.Add(new ChiTietGioHang
            {
                SanPham = product,
                SoLuong = quantity,
            });
            return RedirectToAction("Index");
        }

        // GET: GioHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietGioHang chiTietGioHang = db.ChiTietGioHangs.Find(id);
            if (chiTietGioHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaGH = new SelectList(db.GioHangs, "MaGH", "Email", chiTietGioHang.MaGH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "MaLoaiSP", chiTietGioHang.MaSP);
            return View(chiTietGioHang);
        }

        // GET: GioHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietGioHang chiTietGioHang = db.ChiTietGioHangs.Find(id);
            if (chiTietGioHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietGioHang);
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
