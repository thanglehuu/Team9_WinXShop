using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team9_winxshop.Models;
using Microsoft.AspNet.Identity;

namespace Team9_winxshop.Controllers
{
    public class HomeController : Controller
    {
        private CT25Team19Entities db = new CT25Team19Entities();
        public ActionResult Index()
        {
            var model = db.SanPham.ToList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Profile()
        {
            string id = User.Identity.GetUserId();
            var model = db.AspNetUsers.FirstOrDefault(c => c.Id == id);
            string email = model.Email;
            KhachHang user = db.KhachHang.FirstOrDefault(c => c.Email == email);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile(KhachHang user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return View(user);
        }
    }
}