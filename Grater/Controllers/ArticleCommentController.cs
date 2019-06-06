using Grater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grater.Controllers
{
    public class ArticleCommentController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection form)
        {
            var commentContent = form["CommentContent"].ToString();
            var articleId = int.Parse(form["ArticleId"]);
         //   var ByWho = User.Identity.Name;

            ArticleComment articComment = new ArticleComment()
            {
              
                ArticleId = articleId,
                CommentContent = commentContent,
                ByWho = User.Identity.Name,
               ThisDateTime = DateTime.Now
            };

            _context.ArticleComments.Add(articComment);
            _context.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());

        }
    }

}
