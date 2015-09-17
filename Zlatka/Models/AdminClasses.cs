using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zlatka.Models
{
    public class Article
    {
        public int id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        //public virtual ICollection<Category> Categories { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string Title { get; set; }
    }

    public class Page
    {
        public int id { get; set; }
        public string Title { get; set; }
        public PageTypes Type { get; set; }
        public string Article { get; set; }
        public string Category { get; set; }
    }

    public enum PageTypes
    {
        SinglePage, Blog
    }
}
