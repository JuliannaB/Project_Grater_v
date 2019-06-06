using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grater.Models
{
    public class BeautyArticle: Article
    {
        [Display(Name ="Type of treatment/Brand of Products")]
        public string Genre { get; set; }
    }
}