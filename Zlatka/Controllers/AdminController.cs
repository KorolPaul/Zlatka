using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Zlatka.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Zlatka.Controllers
{
    //[Authorize(Roles = "User")]
    public class AdminController : Controller
    {
        private AdminContext db = new AdminContext();
        private ApplicationDbContext appdb = new ApplicationDbContext();

        public ActionResult Index()
        {
            Roles.CreateRole("members");
            Roles.AddUserToRole("test3@gmail.com", "members");
            var role = User.IsInRole("members");
            if (User.IsInRole("User"))
            {
                return View();
            }
            return RedirectToAction("Index");
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
        public ActionResult AddArticle([Bind(Include = "id,Title,Date,Annotation,Content,CategoryID,Url")] Article article, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    SaveImage(image);
                    article.Image = image.FileName;
                }
                
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Articles");
            }

            return View(article);
        }

        public ActionResult EditArticle(int id)
        {
            Article article = db.Articles.Find(id);
            ViewBag.CategoryID = SelectCategories((int)article.CategoryID);

            return View(article);
        }

        [HttpPost]
        public ActionResult EditArticle([Bind(Include = "id,Title,Date,Annotation,Content,CategoryID,Url")] Article article, HttpPostedFileBase image, string oldImage)
        {
            ViewBag.CategoryID = SelectCategories((int)article.CategoryID);
            if (ModelState.IsValid)
            {
                if (image != null && article.Image != image.FileName)
                {
                    SaveImage(image);
                    article.Image = image.FileName;
                }
                else
                {
                    article.Image = oldImage;
                }

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
        public ActionResult AddCategory([Bind(Include = "id,Title,Url")] Category category, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    SaveImage(image);
                    category.Image = image.FileName;
                }
                
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
        public ActionResult EditCategory([Bind(Include = "id,Title,Url")] Category category, HttpPostedFileBase image, string oldImage)
        {
            if (ModelState.IsValid)
            {
                if (image != null && category.Image != image.FileName)
                {
                    SaveImage(image);
                    category.Image = image.FileName;
                }
                else
                {
                    category.Image = oldImage;
                }

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
        public ActionResult AddPage([Bind(Include = "id,Title,Type,Url,ArticleID,CategoryID,Keywords,Description")] Page page)
        {
            if (ModelState.IsValid)
            {
                page.Url = SaveUrl((int)page.ArticleID, (int)page.CategoryID, page.Type);

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
                page.Url = SaveUrl((int)page.ArticleID, (int)page.CategoryID, page.Type);

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

        public void SaveImage(HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                var ext = Path.GetExtension(image.FileName);
                var path = Server.MapPath("~/Content/Images");
                var file = Path.Combine(path, image.FileName);
                var tmp = Path.GetTempFileName() + "." + ext;

                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    if (System.IO.File.Exists(file))
                    {
                        System.IO.File.Move(file, file.Replace(".jpg", DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".jpg"));
                    }

                    image.SaveAs(file);
                }
                catch (Exception e)
                {
                    throw new HttpException(400, e.Message);
                }
                finally
                {
                    System.IO.File.Delete(tmp);
                }
            }
        }

        public string SaveUrl(int articleId, int categoryId, PageTypes type)
        {
            if (type == PageTypes.SinglePage)
            {
                return (from art in db.Articles where art.id == articleId select art.Url).FirstOrDefault();
            }
            else if (type == PageTypes.Blog)
            {
                return (from cat in db.Categories where cat.id == categoryId select cat.Url).FirstOrDefault();
            }

            return "";
        }

        #region Users
        public ActionResult Users()
        {
            ViewBag.Users = appdb.Users.ToList();
            ViewBag.Roles = appdb.Roles.ToList();
            return View();
        }

        public ActionResult EditUser(string id)
        {
            ApplicationUser user = appdb.Users.Find(id);
            var userRole = user.Roles.FirstOrDefault();
            var userRoleId = "";

            if(userRole != null)
            {
                userRoleId = appdb.Roles.Find(userRole.RoleId).Id;
            }

            SelectList list = new SelectList(appdb.Roles.ToList(), "Id", "Name", userRole);
            ViewBag.Roles = list;

            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser([Bind(Include = "Id,Email")] ApplicationUser user, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                appdb.Entry(user).Entity.UserName = user.Email;
                var account = new AccountController(HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>());
                var role = appdb.Roles.Find(collection["Role"]);

                if(!account.UserManager.IsInRole(user.Id, role.Name)) {
                    if (account.UserManager.GetRoles(user.Id).Count > 0)
                    {
                        account.UserManager.RemoveFromRole(user.Id, account.UserManager.GetRoles(user.Id).FirstOrDefault());
                    }
                    account.UserManager.AddToRole(user.Id, role.Name);
                }
                
                appdb.Entry(user).State = EntityState.Modified;
                appdb.SaveChanges();

                return RedirectToAction("Users");
            }

            return View(user);
        }
        #endregion

        #region Roles
        public ActionResult AddRole(string id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRole(FormCollection collection)
        {
            appdb.Roles.Add(new IdentityRole() { Name = collection["RoleName"] });
            appdb.SaveChanges();

            return RedirectToAction("Users");
        }

        public ActionResult EditRole(string id)
        {
            var role = appdb.Roles.Find(id);
            return View(role);
        }

        [HttpPost]
        public ActionResult EditRole([Bind(Include = "Id,Name")] IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                appdb.Entry(role).State = EntityState.Modified;
                appdb.SaveChanges();
                return RedirectToAction("Users");
            }

            return View(role);
        }

        [HttpPost]
        public JsonResult DeleteRole(string id)
        {
            var role = appdb.Roles.Find(id);
            appdb.Roles.Remove(role);
            appdb.SaveChanges();
            return Json("Deleted");
        }
        #endregion
    }
}