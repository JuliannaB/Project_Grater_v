using Grater.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Grater.Controllers
{
    public class JobController : Controller
    {
        // GET: Job
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Article
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "articleTitle_desc" : "";
            var jobs = from s in _context.Jobs
                           select s;
            switch (sortOrder)
            {
                default:
                    jobs = jobs.OrderBy(s => s.JobTitle);
                    break;
            }
            return View(jobs.ToList());
        }



        // GET: Job/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = _context.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            ViewBag.ArticleId = id.Value;

            var commentContent = _context.Articles.Where(d => d.ArticleId.Equals(id.Value)).ToList();
            ViewBag.CommentContent = commentContent;



            return View(job);
        }




        // GET: Finder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Job/Create
        [HttpPost]
        public ActionResult Create(Job job)
        {
           
            try
            {
                if (ModelState.IsValid)
                {
                    job.ThisDateTime = DateTime.Now;
                    job.WhoAdd = User.Identity.Name;
                    _context.Jobs.Add(job);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //   article.ThisDateTime = DateTime.Now;
            return View(job);
        }

 
    

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = _context.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Job/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Job job)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(job).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        // GET: Job/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Job job = _context.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }


        // POST: Jobs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Job job = _context.Jobs.Find(id);
                _context.Jobs.Remove(job);
                _context.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}

