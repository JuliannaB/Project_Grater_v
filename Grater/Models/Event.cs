using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grater.Models
{
    public class Event: Article
    {
        [Display(Name = "City")]
        public string EventCity { get; set; }
        [Display(Name = "Date of event")]
        [DataType(DataType.Date)]
        public DateTime? EventDate { get; set; }
    }
}