using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grater.Models
{
    public class Finder: Article
    {
  
        [Display(Name = "Seen last time")]
        public string WhenLastSeen { get; set; }
        [Display(Name = "Where")]
        public string WhereLastSeen { get; set; }
        [Display(Name ="Who")]
        public string WhatSpeciality { get; set; }
     
    }
}