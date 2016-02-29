using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Zlatka.Models
{
    public class Training
    {
        public int id { get; set; }
        [AllowHtml]
        public string Content  { get; set; }
        public object ApplicationUserID { get; set; }
        //public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
