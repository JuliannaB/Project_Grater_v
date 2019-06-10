using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Grater.Models
{
    public class ArticleComment
    {
        [Key]
        public int ArtComId { get; set; }

        [Display(Name = "Comment title")]
        public string CommentTitle { get; set; }

        public string CommentContent { get; set; }

        public string ByWho { get; set; }
        public int? Rating { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ThisDateTime { get; set; }
        public Article Article  { get; set; }  //FK to Article
        public int ArticleId { get; set; }     //FK to Article

    }
}