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
            Article article = db.Articles.Find(pageId);

            return View(article);
        }

        public ActionResult ShowMenu()
        {
            return View(db.Pages.ToList());
        }
    }
}