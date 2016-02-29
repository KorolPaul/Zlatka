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
    public class TrainingController : Controller
    {
        private TrainingContext db = new TrainingContext();
        private ApplicationDbContext appdb = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoadTraining(string content)
        {
            Training training = new Training { Content = content };
            db.Trainings.Add(training);
            db.SaveChanges();
            return Json("Saved");
        }

        [HttpPost]
        public JsonResult SaveTraining(string content)
        {
            Training training = new Training {  Content = content };
            db.Trainings.Add(training);
            db.SaveChanges();
            return Json("Saved");
        }

    }
}