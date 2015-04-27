using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zlatka.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.Message = "Your application description page.";
            return View();
        }
    }
}