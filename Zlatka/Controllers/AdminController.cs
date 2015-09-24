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
            ViewBag.CategoryID = SelectCategories();
            return View();
        }

        [HttpPost]
        public ActionResult AddArticle([Bind(Include = "id,Title,Date,Annotation,Content,CategoryID")] Article article)
        {
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
            ViewBag.CategoryID = SelectCategories(article.CategoryID);

            return View(article);
        }

        [HttpPost]
        public ActionResult EditArticle([Bind(Include = "id,Title,Date,Annotation,Content,CategoryID")] Article article)
        {
            ViewBag.CategoryID = SelectCategories(article.CategoryID);
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

        public SelectList SelectArticles(int articleID = 0)
        {
            var articles = from c in db.Articles select c;
            return new SelectList(articles, "id", "Title", articleID);
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
        
        public SelectList SelectCategories(int categoryID = 0){
            var categories = from c in db.Categories select c;
            return new SelectList(categories, "id", "Title", categoryID);
        }
        #endregion

        #region Pages
        public ActionResult Pages()
        {
            return View(db.Pages.ToList());
        }
        public ActionResult AddPage()
        {
            ViewBag.CategoryID = SelectCategories();
            ViewBag.ArticleID = SelectArticles();
            return View();
        }

        [HttpPost]
        public ActionResult AddPage([Bind(Include = "id,Title,Type,Url,ArticleID,CategoryID")] Page page)
        {
            if (ModelState.IsValid)
            {
                db.Pages.Add(page);
                db.SaveChanges();
                return RedirectToAction("Pages");
            }

            return View(page);
        }

        public ActionResult EditPage(int id)
        {
            Page page = db.Pages.Find(id);
            ViewBag.CategoryID = SelectCategories((int)page.CategoryID);
            ViewBag.ArticleID = SelectArticles((int)page.ArticleID);

            return View(page);
        }

        [HttpPost]
        public ActionResult EditPage([Bind(Include = "id,Title,Type,Url,ArticleID,CategoryID")] Page page)
        {
            if (ModelState.IsValid)
            {
                db.Entry(page).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Pages");
            }

            return View(page);
        }

        [HttpPost]
        public JsonResult DeletePage(int id)
        {
            Page page = db.Pages.Find(id);
            db.Pages.Remove(page);
            db.SaveChanges();
            return Json("Deleted");
        }
        #endregion
    }
}