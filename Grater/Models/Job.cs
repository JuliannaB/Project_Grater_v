using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grater.Models
{
    public class Job: Article
    {
       
        [Display(Name ="Job title")]
        public string JobTitle { get; set; }
        public string Localization { get; set; }

    }
}