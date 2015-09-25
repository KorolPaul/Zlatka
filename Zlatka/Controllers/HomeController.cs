using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zlatka.Models;

namespace Zlatka.Controllers
{
    public class HomeController : Controller
    {
        private AdminContext db = new AdminContext();

        public ActionResult Index()
        {
            return View(db.Articles.ToList());
        }

        public ActionResult ShowPage(string url)
        {
            int pageId = (from p in db.Pages where p.Url == url select p.id).FirstOrDefault();

            return View(db.Articles.Find(pageId));
        }

        public ActionResult ShowCategory(string url)
        {
            Page cat = (from p in db.Pages where p.Url == url select p).FirstOrDefault();
            var pages = (from p in db.Articles where p.CategoryID == cat.id select p).ToList();
            ViewBag.Title = cat.Title;

            return View(pages);
        }

        public ActionResult ShowMenu()
        {
            return View(db.Pages.ToList());
        }
    }
}