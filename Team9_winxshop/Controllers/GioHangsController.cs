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
                else
                {
                    hashtable[billdetail.SanPham.MaSP] = billdetail;
                }
            }

            ShoppingCart.Clear();
            foreach(ChiTietGioHang ctgh in hashtable.Values)
            {
                ShoppingCart.Add(ctgh);
            }

            return View(ShoppingCart);
        }

        [AllowAnonymous]
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
        public ActionResult UpdateCart(int[] quantity)
        {
            for(int i = 0; i < ShoppingCart.Count; i++)
            {
                ShoppingCart[i].SoLuong = quantity[i];
            }
            return RedirectToAction("Index");
        }

        public ActionResult CheckOut()
        {
            return View(ShoppingCart);
        }

        public ActionResult Success(FormCollection frc)
        {
            DonHang donhang = new DonHang
            {
                Email = frc["khEmail"],
                MaTT = 1,
                DiaChiNguoiNhan = frc["khDiachi"],
                SDT = frc["khSodt"],
                TongTien = Int32.Parse(frc["TTien"]),
            };
            db.DonHangs.Add(donhang);
            db.SaveChanges();

            foreach (var billdetail in ShoppingCart)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang()
                {
                    MaDH = donhang.MaDH,
                    MaSP = billdetail.SanPham.MaSP,
                    SoLuong = billdetail.SoLuong,
                };
                db.ChiTietDonHangs.Add(ctdh);
                db.SaveChanges();
            }
            ShoppingCart.Clear();
            return View("Success");
        }

        public ActionResult Delete(string productId)
        {
            foreach (var billdetail in ShoppingCart)
            {
                if (billdetail.MaSP == productId)
                {
                    ShoppingCart.Remove(billdetail);
                    break;
                }
            }

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
