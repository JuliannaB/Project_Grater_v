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
    public class EventController : Controller
    {
        // GET: Event
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Article
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "articleTitle_desc" : "";
            var events = from s in _context.Events
                           select s;
            switch (sortOrder)
            {
                default:
                    events = events.OrderBy(s => s.ArticleTitle);
                    break;
            }
            return View(events.ToList());
        }

        public ActionResult AdminIndex(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "articleTitle_desc" : "";
            var events = from s in _context.Events
                                 select s;
            switch (sortOrder)
            {
                default:
                    events = events.OrderBy(s => s.ArticleTitle);
                    break;
            }
            return View(events.ToList());
        }
        // GET: Event/Details/5
        public ActionResult Details(int? id)    
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = _context.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }

            ViewBag.ArticleId = id.Value;

            var commentContent = _context.Articles.Where(d => d.ArticleId.Equals(id.Value)).ToList();
            ViewBag.CommentContent = commentContent;



            return View(@event);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(Event @event)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    @event.ThisDateTime = DateTime.Now;
                    @event.WhoAdd = User.Identity.Name;
                    _context.Events.Add(@event);
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
            return View(@event);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = _context.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(@event).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        [Authorize(Roles = "Admin")]
        // GET: Event/Delete/5
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
            Event @event  = _context.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        [Authorize(Roles = "Admin")]
        // POST: Finders/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Event @event = _context.Events.Find(id);
                _context.Events.Remove(@event);
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
