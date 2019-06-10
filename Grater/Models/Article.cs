using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grater.Models
{
    public abstract class Article
    {
        public int ArticleId { get; set; }
        [Display(Name = "Title")]
        public string ArticleTitle { get; set; }
        [Display(Name ="Added by: ")]
        public string WhoAdd { get; set; }
        [Display(Name ="Main Content")]
        public string ArticleBody { get; set; }
        [Display(Name = "Created date")]
            public DateTime? ThisDateTime { get; set; }
      //  public int WhatIsIt { get; set; }
        public virtual ICollection<ArticleComment> ArticleComments { get; set; }


    }
}