using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grater.Models
{

    public class Comment
    {

        public int Id { get; set; }

        public string CommentTitle { get; set; }
        public string CommentContent { get; set; }

        public string ByWho { get; set; }
        public int ByWhoIn { get; set; }
        public int? Rating { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ThisDateTime { get; set; }
        public Therapist Therapist { get; set; }  //FK to Therapist
        public int TherapistId { get; set; }     //FK to Therapist




    }
}