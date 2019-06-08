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
    public class BeautyArticleController : Controller
    { 

        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Article

            public ActionResult AdminIndex(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "articleTitle_desc" : "";
            var beautyArticles = from s in _context.BeautyArticles
                                 select s;
            switch (sortOrder)
            {
                default:
                    beautyArticles = beautyArticles.OrderBy(s => s.ArticleTitle);
                    break;
            }
            return View(beautyArticles.ToList());
        }
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "articleTitle_desc" : "";
            var beautyArticles = from s in _context.BeautyArticles
                          select s;
            switch (sortOrder)
            {
                default:
                    beautyArticles = beautyArticles.OrderBy(s => s.ArticleTitle);
                    break;
            }
            return View(beautyArticles.ToList());
        }
        // GET: BeautyArticle/Details/5
        public ActionResult Details(int? id)    //Wchodzimy do view details, po klikieciu na imie. Dziala ok
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeautyArticle beautyArticle = _context.BeautyArticles.Find(id);
            if (beautyArticle == null)
            {
                return HttpNotFound();
            }

            ViewBag.ArticleId = id.Value;

            var commentContent = _context.Articles.Where(d => d.ArticleId.Equals(id.Value)).ToList();
            ViewBag.CommentContent = commentContent;



            return View(beautyArticle);
        }


        // GET: BeautyArticle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BeautyArticle/Create
        [HttpPost]
        public ActionResult Create(BeautyArticle beautyArticle)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    beautyArticle.ThisDateTime = DateTime.Now;
                    beautyArticle.WhoAdd = User.Identity.Name;
                    _context.BeautyArticles.Add(beautyArticle);
                    _context.SaveChanges();
                    //    BeautyArticle.ThisDateTime = DateTime.Now;
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //   article.ThisDateTime = DateTime.Now;
            return View(beautyArticle);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeautyArticle beautyArticle = _context.BeautyArticles.Find(id);
            if (beautyArticle == null)
            {
                return HttpNotFound();
            }
            return View(beautyArticle);
        }

        // POST: BeautyArticle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BeautyArticle beautyArticle)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(beautyArticle).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(beautyArticle);
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
            BeautyArticle beautyArticle = _context.BeautyArticles.Find(id);
            if (beautyArticle == null)
            {
                return HttpNotFound();
            }
            return View(beautyArticle);
        }


        // POST: BeautyArticles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                BeautyArticle beautyArticle = _context.BeautyArticles.Find(id);
                _context.BeautyArticles.Remove(beautyArticle);
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
