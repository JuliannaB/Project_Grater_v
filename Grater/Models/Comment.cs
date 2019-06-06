using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grater.Models
{

    public class Comment
    {

        public int Id { get; set; }

        public string CommentTitle { get; set; }
        public string CommentContent { get; set; }

        public int? Rating { get; set; }
        public DateTime? ThisDateTime { get; set; }
        public Therapist Therapist { get; set; }  //FK to Therapist
        public int TherapistId { get; set; }     //FK to Therapist




    }
}