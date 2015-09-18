using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zlatka.Models;

namespace Zlatka.Controllers
{
    public class AdminController : Controller
    {
        private AdminContext db = new AdminContext();

        public ActionResult Index()
        {
            return View();
        }

        #region Articles
        public ActionResult Articles()
        {
            return View(db.Articles.ToList());
        }
        public ActionResult AddArticle()
        {
            ViewBag.Categories = from c in db.Categories select c.id;
            return View();
        }

        [HttpPost]
        public ActionResult AddArticle([Bind(Include = "id,Title,Date,Content,CategoryID")] Article article)
        {
            ViewBag.Categories = from c in db.Categories select c.id;
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Articles");
            }

            return View(article);
        }

        public ActionResult EditArticle(int id)
        {
            Article article = db.Articles.Find(id);
            return View(article);
        }

        [HttpPost]
        public ActionResult EditArticle([Bind(Include = "id,Title,Date,Content")] Article article)
        {
            ViewBag.Categories = db.Categories.ToList();
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Articles");
            }

            return View(article);
        }

        [HttpPost]
        public JsonResult DeleteArticle(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return Json("Deleted");
        }
        #endregion

        #region Categories
        public ActionResult Categories()
        {
            return View(db.Categories.ToList());
        }
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory([Bind(Include = "id,Title")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Categories");
            }

            return View(category);
        }

        public ActionResult EditCategory(int id)
        {
            Category category = db.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult EditCategory([Bind(Include = "id,Title")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Categories");
            }

            return View(category);
        }

        [HttpPost]
        public JsonResult DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return Json("Deleted");
        }
        #endregion
    }
}