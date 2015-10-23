using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Zlatka.Models
{
    public class Category
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Image { get; set; } 
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Page> Pages { get; set; }
    }

    public class Article
    {
        public int id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public string Image { get; set; } 
        public string Annotation { get; set; }
        [AllowHtml] 
        public string Content { get; set; }
        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Page> Pages { get; set; }
    }


    public class Page
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public PageTypes Type { get; set; }
        public int? ArticleID { get; set; }
        public virtual Article Article { get; set; }
        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }

    public enum PageTypes
    {
        SinglePage, Blog
    }
}
