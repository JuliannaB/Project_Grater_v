using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Web.UI.WebControls;
using Grater.Models;
using Microsoft.AspNet.Identity;

namespace Grater.Controllers
{
    public class CommentController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection form)
        {
            var commentContent = form["CommentContent"].ToString();
            var therapistId = int.Parse(form["TherapistId"]);
            var rating = int.Parse(form["Rating"]);
            var WhoAddInt = User.Identity.GetUserId<int>();
            Comment therComment = new Comment()
            {
                TherapistId = therapistId,
                CommentContent = commentContent,
                Rating = rating,
                ByWho = User.Identity.Name,
                ByWhoIn = WhoAddInt,
                ThisDateTime = DateTime.Now

            };

            _context.Comments.Add(therComment);
            _context.SaveChanges();

            return RedirectToAction("Details", "Therapist", new { id = therapistId });

        }


 
        // POST: Comment/Create







        /*
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        // GET: Comment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comment/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Comment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Comment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Comment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }  */
    }

}
