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
    public class HomeController : Controller
    {
        private CT25Team19Entities db = new CT25Team19Entities();
        public ActionResult Index()
        {
            var model = db.SanPhams.ToList();
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
    }
}