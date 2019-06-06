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
    public class FinderController : Controller
    {
        // GET: Finder

        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Article
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "articleTitle_desc" : "";
            var finders = from s in _context.Finders
                          select s;
            switch (sortOrder)
            {
                default:
                    finders = finders.OrderBy(s => s.ArticleTitle);
                    break;
            }
            return View(finders.ToList());
        }
        // GET: Finder/Details/5
        public ActionResult Details(int? id)    //Wchodzimy do view details, po klikieciu na imie. Dziala ok
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Finder finder = _context.Finders.Find(id);
            if (finder == null)
            {
                return HttpNotFound();
            }

            ViewBag.ArticleId = id.Value;

            var commentContent = _context.Articles.Where(d => d.ArticleId.Equals(id.Value)).ToList();
            ViewBag.CommentContent = commentContent;

          

            return View(finder);
        }


        // GET: Finder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Finder/Create
        [HttpPost]
        public ActionResult Create(Finder finder)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    finder.ThisDateTime = DateTime.Now;
                    finder.WhoAdd = User.Identity.Name;
                    _context.Finders.Add(finder);
                    _context.SaveChanges();
                //    finder.ThisDateTime = DateTime.Now;
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //   article.ThisDateTime = DateTime.Now;
            return View(finder);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Finder finder = _context.Finders.Find(id);
            if (finder == null)
            {
                return HttpNotFound();
            }
            return View(finder);
        }

        // POST: Finder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Finder finder)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(finder).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(finder);
        }


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
            Finder finder = _context.Finders.Find(id);
            if (finder == null)
            {
                return HttpNotFound();
            }
            return View(finder);
        }


        // POST: Finders/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Finder finder = _context.Finders.Find(id);        
                _context.Finders.Remove(finder);
                _context.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
