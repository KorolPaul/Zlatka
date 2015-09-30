using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Zlatka.Models
{
    public class Training
    {
        public int id { get; set; }
        public string Content  { get; set; }
        public int ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
